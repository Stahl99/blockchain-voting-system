const BVS_backend = artifacts.require("bvs_backend");

contract("bvs_backend", (accounts) => {
  let instance;

  // parameters for the elections
  let name1 = "test election 1";
  let name2 = "test election 2";
  let name3 = "test election 3";

  let startTimestamp1 = Math.floor (Date.now() / 1000);
  let startTimestamp2 = Math.floor (Date.now() / 1000) + 10000;
  let endTimestamp1 = (Math.floor(Date.now() / 1000)) + 9000000;
  let endTimestamp2 = (Math.floor(Date.now() / 1000)) + 9500000;

  beforeEach(async () => {
    instance = await BVS_backend.deployed();

    // create elections for the test
    // admin addresses are test users
    // two are first past the post; middle one is alternative voting
    // timestamps are now and a time a few days in the future
    await instance.createElection("0x3D350e8FAdDFE27C33CD16c03178326E82012b4c", 0, name1, startTimestamp1, endTimestamp1);
    await instance.createElection("0x861E2334a5A0Ab09Db7d2E49cb4f1c138dA55253", 1, name2, startTimestamp1, endTimestamp1);
    await instance.createElection(accounts[0], 0, name3, startTimestamp2, endTimestamp2);
    
  });

  // This code tests the following functions:
  // getLastElectionId
  it("test correct return of last election id", async () => {
    // with 3 elections the last id should be 2
    assert.equal(await instance.getLastElectionId(), 2, "id of the last election should be 0");
  });

  // This code tests the following functions:
  // getElectionInformation
  it("test correct return of correct election information", async () => {

    let elections = await instance.getElectionInformation(); // get election info

    // test all infos and set bool to true if correct
    let electionInformationCorrect = false;
    if (elections[0].id == 0 && elections[0].name == name1 /* test first election */
      && elections[0].startTimestamp == startTimestamp1 && elections[0].endTimestamp == endTimestamp1 && elections[0].votingSystem == 0 && 
      elections[1].id == 1 && elections[1].name == name2 /* test second election */
      && elections[1].startTimestamp == startTimestamp1 && elections[1].endTimestamp == endTimestamp1 && elections[1].votingSystem == 1 && 
      elections[2].id == 2 && elections[2].name == name3 /* test third election */
      && elections[2].startTimestamp == startTimestamp2 && elections[2].endTimestamp == endTimestamp2 && elections[2].votingSystem == 0) {
        electionInformationCorrect = true;
      }

    assert.equal(electionInformationCorrect, true, "All returned infos about the elections are correct");
  });

  // This code tests the following functions:
  // replaceListOfEligibleVoters
  it("test correct replacement of the list of eligible voters", async () => {

    let voterAdresses = ["0x861E2334a5A0Ab09Db7d2E49cb4f1c138dA55253", "0x3D350e8FAdDFE27C33CD16c03178326E82012b4c"]; // enter two eligible voters
    let lastElectionId = await instance.getLastElectionId(); // get the id of the last created election
    // replace all eligible voters from the last election with the list above
    let response = await instance.replaceListOfEligibleVoters (lastElectionId, voterAdresses); 
    assert.equal(response.receipt.status, true, "replaceListOfEligibleVoters returns true (does not mean that it has been replaced correctly)");

  });

  // This code tests the following functions:
  // replaceElectoralList
  // getElectoralList
  it("tests replaceElectoralList and getElectoralList", async () => {
    
    let testSuccessful = false;
    let definiedElectoralList = [{firstName:'first1', lastName:'last1', party:'party1', id: 0},{firstName:'first2', lastName:'last2', party:'party2', id: 1}];
    let lastElectionId = await instance.getLastElectionId(); // get the id of the last created election

    let replaceElectoralListStatus = await instance.replaceElectoralList(lastElectionId, definiedElectoralList, {from: accounts[0]});
    let returnedElectoralList = await instance.getElectoralList(lastElectionId);

    if (returnedElectoralList[0].firstName == "first1" && returnedElectoralList[0].lastName == "last1" && returnedElectoralList[0].party == "party1" && returnedElectoralList[0].id == 0 &&
        returnedElectoralList[1].firstName == "first2" && returnedElectoralList[1].lastName == "last2" && returnedElectoralList[1].party == "party2" && returnedElectoralList[1].id == 1 &&
        replaceElectoralListStatus) {
          testSuccessful = true;
    }

    assert.equal(testSuccessful, true, "The returned electoral list is correct");
  });

  // This code tests the following functions:
  // vote
  it("should reject vote from non-eligible voters", async () => {
    let electionId = 0;
    let candidateId = 1;
    const ranking = [];
    await truffleAssert.reverts(instance.testVote(electionId, candidateId, ranking,
      {from: accounts[2]}));
  });

  // This code tests the following functions
  // vote
  // getVoteBallot
  it("should accept a valid vote", async () => {
    let electionId = 0;
    let candidateId = 1;
    const ranking = [];
    let response = await instance.testVote(electionId, candidateId, ranking, {from: accounts[1]});
    assert.equal(response.receipt.status, true, "Function should return true");
  });

  // This code tests the following functions
  it("should reject a second vote", async () => {
    let electionId = 0;
    let candidateId = 1;
    const ranking = [];
    await truffleAssert.reverts(instance.testVote(electionId, candidateId, ranking,
      {from: accounts[1]}));
  });

  // This code tests the following functions:
  // countVotes
  it("should count the votes correctly", async () => {
    // I have no idea what I'm doing
  });

});
