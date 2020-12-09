const BVS_backend = artifacts.require("bvs_backend");

contract("bvs_backend", (accounts) => {
  let instance;

  // parameters for the elections
  let name1 = "test election 1";
  let name2 = "test election 2";
  let name3 = "test election 3";

  let startTimestamp1 = Math.floor(Date.now() / 1000);
  let startTimestamp2 = Math.floor(Date.now() / 1000) + 10000;
  let endTimestamp1 = (Math.floor(Date.now() / 1000)) + 9000000;
  let endTimestamp2 = (Math.floor(Date.now() / 1000)) + 9500000;

  beforeEach(async () => {
    instance = await BVS_backend.deployed();

    // create elections for the test
    // admin addresses are test users
    // two are first past the post; middle one is alternative voting
    // timestamps are now and a time a few days in the future
    await instance.createElection("0x3D350e8FAdDFE27C33CD16c03178326E82012b4c", 0, name1, startTimestamp1, endTimestamp1);
    await instance.createElection("0x861E2334a5A0Ab09Db7d2E49cb4f1c138dA55253", 1, name2, startTimestamp2, endTimestamp2);
    await instance.createElection(accounts[0], 0, name3, startTimestamp1, endTimestamp1);

  });

  // This code tests the following functions:
  // getLastElectionId
  it("test correct return of last election id", async () => {
    // with 3 elections the last id should be 2
    assert.equal(await instance.getLastElectionId(), 2, "id of the last election should be 2");
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
      && elections[1].startTimestamp == startTimestamp2 && elections[1].endTimestamp == endTimestamp2 && elections[1].votingSystem == 1 &&
      elections[2].id == 2 && elections[2].name == name3 /* test third election */
      && elections[2].startTimestamp == startTimestamp1 && elections[2].endTimestamp == endTimestamp1 && elections[2].votingSystem == 0) {
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
    let response = await instance.replaceListOfEligibleVoters(lastElectionId, voterAdresses);
    assert.equal(response.receipt.status, true, "replaceListOfEligibleVoters returns true (does not mean that it has been replaced correctly)");

  });

  // This code tests the following functions:
  // replaceElectoralList
  // getElectoralList
  it("tests replaceElectoralList and getElectoralList", async () => {

    let testSuccessful = false;
    let definiedElectoralList = [{ firstName: 'first1', lastName: 'last1', party: 'party1', id: 0 }, { firstName: 'first2', lastName: 'last2', party: 'party2', id: 1 }];
    let lastElectionId = await instance.getLastElectionId(); // get the id of the last created election

    let replaceElectoralListStatus = await instance.replaceElectoralList(lastElectionId, definiedElectoralList, { from: accounts[0] });
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
    await prepareElection();
    let electionId = 2;
    let candidateId = 0;
    const ranking = [0, 0];
    try {
      await instance.vote(electionId, { voterAddress: accounts[3], candidateId: candidateId, ranking: ranking },
        { from: accounts[3] });
      throw null;
    }
    catch (error) {
      assert(error, "Expected error but did not get one");
    }
  });

  // This code tests the following functions
  // vote
  // getVoteBallot
  it("should accept a valid vote", async () => {
    await prepareElection();
    const ranking = [0, 0];
    let ballot = { voterAddress: accounts[1], candidateId: 0, ranking: ranking };
    let response = await instance.vote(2, ballot, { from: accounts[1] });
    assert.equal(response.receipt.status, true, "vote() should return true on success");
    returnedBallot = await instance.getVoteBallot(2);
    assert.equal(returnedBallot.candidateId, ballot.candidateId, "Ballot is not returned correctly");
  });

  // This code tests the following functions
  // vote
  it("should recect a second vote", async () => {
    await storeVote();
    const ranking = [0, 0];
    let ballot = { voterAddress: accounts[1], candidateId: 0, ranking: ranking };
    try {
      await instance.vote(2, ballot, { from: accounts[1] });
      throw null;
    }
    catch (error) {
      assert(error, "Expected error but did not get one");
    }

  });

  // This code tests the following functions:
  // countVotes
  it("should count the votes correctly", async () => {
    await storeVote();
    await instance.countVotes(2);
    let response = await instance.getResult(2);
    assert.equal(response.candidates[1].firstName, 'first1', "Candidates are not in correct order");
  });

  async function prepareElection() {
    // Set eligible voters
    let voterAdresses = [accounts[1], accounts[2]];
    await instance.replaceListOfEligibleVoters(2, voterAdresses, { from: accounts[0] });

    // Set electoral list
    let definiedElectoralList = [{ firstName: 'first1', lastName: 'last1', party: 'party1', id: 0 }, { firstName: 'first2', lastName: 'last2', party: 'party2', id: 1 }];
    await instance.replaceElectoralList(2, definiedElectoralList, { from: accounts[0] });
  }

  async function storeVote() {
    prepareElection();
    let electionId = 2;
    const ranking = [0, 0];
    let ballot = { voterAddress: accounts[1], candidateId: 0, ranking: ranking };
    try {
      await instance.vote(electionId, ballot, { from: accounts[1] });
    }
    catch {
      /* Address may already has voted, do not fail test because of this */
    }
  }

});
