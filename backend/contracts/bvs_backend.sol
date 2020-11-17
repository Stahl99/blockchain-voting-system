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
        string electionName; // name describing the election

        Candidate[] electoralList; // all candidates in the election 
        Ballot[] ballots; // all cast votes
        address[] eligibleVoters; // list of all eligible voters
        address[] usedAddresses; // all addresses that already voted
        VotingSystem votingSystem; // type of the voting system

        address adminAddress; // wallet address of the admin

        // unix timestamps
        uint256 startTimestamp;
        uint256 endTimestamp;
    }

    Election[] private _elections; // array of all elections
    uint256 private currentElectionId; // global election id counter

    constructor() {
        currentElectionId = 0; // initialize the global election id counter
    }

    // creates a new election and returns the election id to the caller
    function createElection (address electionAdminAddress, VotingSystem electionVotingSystem, string memory electionName,
    uint256 electionStartTimestamp, uint256 electionEndTimestamp) public returns (uint256) {
        
        // create new election and set values
        Election memory e;
        e.electionId = currentElectionId++; // set and increment id 
        e.electionName = electionName;
        e.votingSystem = electionVotingSystem;
        e.adminAddress = electionAdminAddress;
        e.startTimestamp = electionStartTimestamp;
        e.endTimestamp = electionEndTimestamp;

        _elections.push(e); // save the new election on the blockchain

        return e.electionId; // return the id of the new election
    }

    // replaces the list of the current eligible voters for a given election
    // returns true if successfull; false otherwise
    function replaceListOfEligibleVoters (uint256 electionId, address[] memory newEligibleVoterList) public returns (bool) {

        for (uint i = 0; i < currentElectionId; i++) {

            // find the correct election
            if (_elections[i].electionId == electionId) {

                // check if the request was sent from the admin account of the election
                if (_elections[i].adminAddress == msg.sender) {
                    // replace the current list of eligible voters with the new one and return true
                    _elections[i].eligibleVoters = newEligibleVoterList;
                    return true;
                }
                else {
                    return false; // if the request was made not by the admin return false
                }

            }
        }

        // return false if the election was not found 
        return false;

    }

    // replaces the list of the current electoral list for a given election
    // returns true if successfull; false otherwise
    function replaceElectoralList (uint256 electionId, Candidate[] memory newElectoralList) public returns (bool) {

        for (uint i = 0; i < currentElectionId; i++) {

            // find the correct election
            if (_elections[i].electionId == electionId) {

                // check if the request was sent from the admin account of the election
                if (_elections[i].adminAddress == msg.sender) {
                    // replace the current electoral list with the new one and return true
                    _elections[i].electoralList = newElectoralList;
                    return true;
                }
                else {
                    return false; // if the request was made not by the admin return false
                }

            }
        }

        // return false if the election was not found 
        return false;

    }

    // returns the ids, names, start- and end-timestamps of all elections
    function getElectionInformation () public view returns (uint256[] memory, string[] memory, uint256[] memory, uint256[] memory) {

        // creates arrays for all values that have to be returned
        // the size of the arrays is equal to the number of current election
        uint256[] memory ids = new uint256[](currentElectionId-1);
        string[] memory names = new string[](currentElectionId-1);
        uint256[] memory startTimestamps = new uint256[](currentElectionId-1);
        uint256[] memory endTimestamps = new uint256[](currentElectionId-1);

        // go through all elections
        for (uint i = 0; i < currentElectionId; i++) {

            // save the election elements to the corresponding arrays
            ids[i] = _elections[i].electionId;
            names[i] = _elections[i].electionName;
            startTimestamps[i] = _elections[i].startTimestamp;
            endTimestamps[i] = _elections[i].endTimestamp;

        }

        // return the lists
        return (ids, names, startTimestamps, endTimestamps);

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

    function getVote (uint256 electionId) public returns (string memory) {
        require(_elections[electionId].usedAddresses[msg.sender] == true, "No vote submitted");
        Ballot memory ballot;
        for (uint256 i = 0; i <= _elections[electionId].ballots.length; i++) {
            ballot = _elections[electionId].ballots[i];
            if (ballot.voterAddress == msg.sender) {
                break;
            }
        }
        if (_elections[electionId].votingSystem == VotingSystem.standardVoting) {
            return string(abi.encode("You voted for ", _elections[electionId].electoralList[ballot.candidateId].firstName,
                " ", _elections[electionId].electoralList[ballot.candidateId].lastName));
        }
    }

    function countVotes (uint256 electionId) public returns (Candidate[] memory, uint256[] memory) {
        
    }

    function isOver (uint256 electionId) public view returns (bool) {
        return (_elections[electionId].endTimestamp < block.timestamp);
    }

}