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

      // get accounts
      const accounts = await web3.eth.getAccounts();
      this.account = accounts[0];

      this.loadElections();
    } catch (error) {
      console.error("Could not connect to contract or chain.");
    }
  },

  loadElections: async function () {

    const electionInfo = await this.bvs_backend.methods.getElectionInformation().call();

    var ul = document.getElementById("election-list");

    var i;
    for (var election of electionInfo) {

      var votingSystemString = "";
      if (election.votingSystem == 1) {
        votingSystemString = "alternative voting";
      }
      else {
        votingSystemString = "standard voting";
      }

      var li = document.createElement("li");
      li.setAttribute('id', election.name);
      li.appendChild(document.createTextNode(election.name + " (" + votingSystemString + ")"));

      ul.appendChild(li);
      const electoralList = await this.bvs_backend.methods.getElectoralList(election.id).call();
      await this.bvs_backend.methods.countVotes(election.id).send({ from: this.account });
      const electionResult = await this.bvs_backend.methods.getResult(election.id).call();

      var newUl;
      if (electoralList.length > 0) {
        newUl = document.createElement("ul");
        newUl.setAttribute('id', election.name + "-candidate-list");
        li.appendChild(newUl);
      }

     electionResult.candidates.forEach(function (candidate, i) {
        var numberOfVotes = 0;
        for (var j = 0; j < electionResult.candidates.length; j++) {
          if (candidate.id == electionResult.candidates[j].id) {
            numberOfVotes = electionResult.votes[j];
          }
        }

        var newLi = document.createElement("li");
        newLi.setAttribute('id', candidate.firstName + candidate.lastName);
        newLi.appendChild(document.createTextNode(candidate.firstName + " " + candidate.lastName + ": " + candidate.party + " (votes: " + numberOfVotes + ")"));
        newUl.appendChild(newLi);
      });
    }
  },

  /*refreshBalance: async function () {
    const { balanceOf, decimals } = this.bvs_backend.methods;
    const balance = await balanceOf(this.account).call();
    const decimal = await decimals().call();

    const balanceElement = document.getElementsByClassName("balance")[0];
    balanceElement.innerHTML = `${balance / Math.pow(10, decimal)}.${(
      balance % 100
    )
      .toString()
      .padStart(2, "0")}`;
  },

  sendCoin: async function () {
    const amount = parseInt(document.getElementById("amount").value);
    const receiver = document.getElementById("receiver").value;

    this.setStatus("Initiating transaction... (please wait)");

    const { transfer } = this.dhbw.methods;
    await transfer(receiver, amount * 100).send({ from: this.account });

    this.setStatus("Transaction complete!");
    this.refreshBalance();
  },

  setStatus: function (message) {
    const status = document.getElementById("status");
    status.innerHTML = message;
  },*/
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
