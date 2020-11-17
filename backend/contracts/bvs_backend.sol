pragma solidity ^0.7.2;
pragma experimental ABIEncoderV2;

contract bvs_backend {

    struct Candidate {
        string firstName;
        string lastName;
        string party;
        uint256 id;
    }

    enum VotingSystem { standardVoting, alternativeVoting }

    struct Ballot {
        // standardVoting 
        uint256 candidateId;

        // alternativeVoting
        uint256[] ranking; // by candidate id
    }

    struct Election {
        uint256 electionId;
        string electionName;

        Candidate[] electoralList; 
        Ballot[] ballots;
        mapping (address => bool) eligibleVoters;
        mapping (address => bool) usedAddresses;
        VotingSystem votingSystem;

        address adminAddress;

        uint256 startTimestamp;
        uint256 endTimestamp;
    }

    Election[] private _elections;

    constructor() {}

    function createElection (address electionAdminAddress, VotingSystem electionVotingSystem, string memory electionName,
    uint256 electionStartTime, uint256 electionEndTimestamp) public view returns (uint256) {

    }

    function vote (uint256 electionId, Ballot memory ballot) public returns (bool) {
        require(_elections[electionId].eligibleVoters[msg.sender] == true, "");

        return true;
    }
}