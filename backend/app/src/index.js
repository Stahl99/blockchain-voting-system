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
      li.appendChild(document.createTextNode(election.name + " (" + votingSystemString + ")"));
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
      searchArray.forEach(function (candidate, i) {

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
