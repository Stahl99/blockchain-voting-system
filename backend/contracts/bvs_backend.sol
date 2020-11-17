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
        // used if a voter wants to check 
        // whether his voted was counted correctly
        address voterAddress; 

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

    // creates a new election and returns the election id to the caller
    function createElection (address electionAdminAddress, VotingSystem electionVotingSystem, string memory electionName,
    uint256 electionStartTime, uint256 electionEndTimestamp) public view returns (uint256) {
        
    }

    // replaces the list of the current eligible voters for a given election
    // returns true if successfull; false otherwise
    function replaceListOfEligibleVoters (uint256 electionId, address[] memory newEligibleVoterList) public view returns (bool) {

    }

    // replaces the list of the current electoral list for a given election
    // returns true if successfull; false otherwise
    function replaceElectoralList (uint256 electionId, Candidate[] memory electoralList) public view returns (bool) {

    }

    // returns the ids, names, start- and end-timestamps of all elections
    function getElectionInformation () public view returns (uint256[] memory, string[] memory, uint256[] memory, uint256[] memory) {

    }

    function vote (uint256 electionId, Ballot memory ballot) public returns (bool) {
        // Check if the address is allowed to vote
        require(_elections[electionId].eligibleVoters[msg.sender] == true, "Address not allowed for voting");
        // Check if the address has already been used
        require(_elections[electionId].usedAddresses[msg.sender] == false, "Address was already used");
        // Check the election time
        require(_elections[electionId].startTimestamp < block.timestamp, "Voting has not started yet");
        require(_elections[electionId].endTimestamp > block.timestamp, "Voting already ended");

        // Add the ballot to the election
        _elections[electionId].ballots.push(ballot);

        // Remember the address has voted
        _elections[electionId].usedAddresses[msg.sender] = true;

        return true;
    }

}