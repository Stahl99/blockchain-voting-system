import Web3 from "web3";
import bvs_backendArtifact from "../../build/contracts/bvs_backend.json";

const App = {
  web3: null,
  account: null,
  bvs_backend: null,

  start: async function () {
    const { web3 } = this;

    try {
      // get contract instance
      const networkId = await web3.eth.net.getId();
      const deployedNetwork = bvs_backendArtifact.networks[networkId];
      this.bvs_backend = new web3.eth.Contract(
        bvs_backendArtifact.abi,
        deployedNetwork.address
      );

      // get accounts (needed for countVotes function)
      const accounts = await web3.eth.getAccounts();
      this.account = accounts[0];

      this.loadElections();
    } catch (error) {
      console.error("Could not connect to contract or chain.");
    }
  },

  checkBallot: async function () {

    // set default text
    var textElement = document.getElementById("ballot");
    textElement.innerHTML = "No response from the blockchain yet...";

    // get id and voter key from user
    const electionId = parseInt(document.getElementById("electionId").value);
    const voterKey = document.getElementById("voterKey").value;

    // check if paramters have been entered
    if (!electionId || !voterKey) {
      textElement.innerHTML = "At least one parameter missing. Please (re-)enter parameters.";
    }

    // get account and ballot
    var account = this.web3.eth.accounts.privateKeyToAccount(voterKey);
    const ballot = await this.bvs_backend.methods.getVoteBallot(electionId).call({ from: account.address });
    
    // get election info
    const electionInfo = await this.bvs_backend.methods.getElectionInformation().call();

    for (var election of electionInfo) {
      if (election.id == electionId) { // find right election

        // get electoral list
        const electoralList = await this.bvs_backend.methods.getElectoralList(election.id).call();

        // set correct text
        var ballotString = "You gave ";
        if (election.votingSystem == 1) {
          
          if (ballot.ranking.length == 0) {
            ballotString = "You did not vote."; // check if user did not vote
          } else {

            // to display the ranking this loop loops over all candidates and displays their rank
            electoralList.forEach(function (candidate, i) {
              if (i == electoralList.length-1) {
                ballotString += candidate.firstName + " " + candidate.lastName + " (" + candidate.party + ") rank " + ballot.ranking[i]; 
              } else if (i == electoralList.length-2) { 
                ballotString += candidate.firstName + " " + candidate.lastName + " (" + candidate.party + ") rank " + ballot.ranking[i] + " and "; 
              } else {
              ballotString += candidate.firstName + " " + candidate.lastName + " (" + candidate.party + ") rank " + ballot.ranking[i] + ", "; 
              }
            });
          }

        } else {
          
          // if the user did not vote in standard voting the voter address is 0
          if (ballot.voterAddress == "0x0000000000000000000000000000000000000000") {
            ballotString = "You did not vote.";
          } else {

            // find the candidate the user voted for and display the ballot
            electoralList.forEach(function (candidate, i) {
              if (candidate.id == ballot.candidateId) {
                ballotString += candidate.firstName + " " + candidate.lastName + " (" + candidate.party + ") your vote.";
              }
            });
          }

        }

        // display the generated string
        textElement.innerHTML = ballotString;

      } else {
        // display this text if no election with the given id was found
        textElement.innerHTML = "Election with id: " + electionId + " not found.";
      }
    }

  },

  loadElections: async function () {

    // get basic info about the elections
    const electionInfo = await this.bvs_backend.methods.getElectionInformation().call();

    // create list for the elections
    var ul = document.getElementById("election-list");

    // iterate over all elections
    for (var election of electionInfo) {

      // set the voting system string for display
      var votingSystemString = "";
      if (election.votingSystem == 1) {
        votingSystemString = "alternative voting";
      }
      else {
        votingSystemString = "standard voting";
      }

      // create list element for the election
      var li = document.createElement("li");
      li.setAttribute('id', election.name);
      li.appendChild(document.createTextNode(election.name + " (" + votingSystemString + "; election id: " + election.id + ")"));
      ul.appendChild(li);

      // get the electoral list of the election
      const electoralList = await this.bvs_backend.methods.getElectoralList(election.id).call();

      // count all votes and get the current standing / result
      await this.bvs_backend.methods.countVotes(election.id).send({ from: this.account });
      const electionResult = await this.bvs_backend.methods.getResult(election.id).call();

      // create a new list for the candidates if there are any
      var newUl;
      if (electoralList.length > 0) {
        newUl = document.createElement("ul");
        newUl.setAttribute('id', election.name + "-candidate-list");
        li.appendChild(newUl);
      }

      // if no one has voted election result is empty
      // if this is the case the electoral list is used for display
      // otherwise the election result array is used as it is already sorted correctly
      var searchArray;
      if (electionResult.candidates.length > 0) {
        searchArray = electionResult.candidates;
      } else {
        searchArray = electoralList;
      }

      // iterate over the candidates
      searchArray.slice().reverse().forEach(function (candidate, i) {

        var numberOfVotes = 0; 
        // check how many votes a candidate has
        for (var j = 0; j < electionResult.candidates.length; j++) {
          if (candidate.id == electionResult.candidates[j].id) {
            numberOfVotes = electionResult.votes[j];
          }
        }

        // create a new list element for the candidate
        var newLi = document.createElement("li");
        newLi.setAttribute('id', candidate.firstName + candidate.lastName);

        newLi.appendChild(document.createTextNode(candidate.firstName + " " + candidate.lastName + ": " + candidate.party + " (votes: " + numberOfVotes + ")"));

        newUl.appendChild(newLi);
      });
    }
  },
};

window.App = App;

window.addEventListener("load", function () {
  if (window.ethereum) {
    // use MetaMask's provider
    App.web3 = new Web3(window.ethereum);
    window.ethereum.enable(); // get permission to access accounts
  } else {
    console.warn(
      "No web3 detected. Falling back to http://127.0.0.1:7545. You should remove this fallback when you deploy live"
    );
    // fallback - use your fallback strategy (local node / hosted node + in-dapp id mgmt / fail)
    App.web3 = new Web3(
      new Web3.providers.HttpProvider("http://127.0.0.1:7545")
    );
  }

  App.start();
});
