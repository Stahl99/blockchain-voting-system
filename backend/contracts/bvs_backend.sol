/* SPDX-License-Identifier: UNLICENSED */
pragma solidity ^0.7.2;
pragma experimental ABIEncoderV2;

// Sorting array off-chain for optimization
// Sorts both arrays (votes and candidates) according to the amount of votes using QuickSort
function sortVotes (uint256[] memory votes, bvs_backend.Candidate[] memory candidates, int left, int right) pure {
    int i = left;
    int j = right;
    if (i == j) return;
    uint pivot = votes[uint(left + (right - left) / 2)];
    while (i <= j) {
        while (votes[uint(i)] < pivot) i++;
        while (pivot < votes[uint(j)]) j--;
        if (i <= j) {
            (votes[uint(i)], votes[uint(j)]) = (votes[uint(j)], votes[uint(i)]);
            (candidates[uint(i)], candidates[uint(j)]) = (candidates[uint(j)], candidates[uint(i)]);
            i++;
            j--;
        }
    }
    if (left < j)
        sortVotes(votes, candidates, left, j);
    if (i < right)
        sortVotes(votes, candidates, i, right);
}

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
        uint256[] votes; // stores votes for according candidates (prio 1 for alternative voting) 
        Ballot[] ballots; // all cast votes
        address[] eligibleVoters; // list of all eligible voters
        address[] usedAddresses; // all addresses that already voted
        VotingSystem votingSystem; // type of the voting system

        address adminAddress; // wallet address of the admin

        // unix timestamps
        uint256 startTimestamp;
        uint256 endTimestamp;
    }

    struct TmpElectionObject {
        uint256 id;
        string name;
        uint256 startTimestamp;
        uint256 endTimestamp;
        VotingSystem votingSystem;
    }

    Election[] private _elections; // array of all elections
    Election temp; // temporary election storage

    constructor() {}

    // creates a new election and returns the election id to the caller
    function createElection (address electionAdminAddress, VotingSystem electionVotingSystem, string memory electionName,
    uint256 electionStartTimestamp, uint256 electionEndTimestamp) public {
        
        // set values in temporary election
        temp.electionId = _elections.length; // set and increment id 
        temp.electionName = electionName;
        temp.votingSystem = electionVotingSystem;
        temp.adminAddress = electionAdminAddress;
        temp.startTimestamp = electionStartTimestamp;
        temp.endTimestamp = electionEndTimestamp;

        _elections.push(temp); // save the new election on the blockchain

    }

    function getLastElectionId () public view returns (uint256) {
        return _elections.length-1;
    }

    // replaces the list of the current eligible voters for a given election
    // returns true if successfull; false otherwise
    function replaceListOfEligibleVoters (uint256 electionId, address[] memory newEligibleVoterList) public returns (bool) {

        for (uint i = 0; i < _elections.length; i++) {

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

        for (uint i = 0; i < _elections.length; i++) {

            // find the correct election
            if (_elections[i].electionId == electionId) {

                // check if the request was sent from the admin account of the election
                if (_elections[i].adminAddress == msg.sender) {
                    // replace the current electoral list with the new one and return true
                    delete _elections[i].electoralList;
                    delete _elections[i].votes;
                    for (uint256 j = 0; j < newElectoralList.length; j++) {
                        _elections[i].electoralList.push(newElectoralList[j]);
                        _elections[i].votes[j] = 0; // Set vote count for each candidate to 0
                    }
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
    // in a temporary election object with all needed return values
    // this is done so that the C# code can be generated better
    function getElectionInformation () public view returns (TmpElectionObject[] memory) {

        // creates tmp election object with all elements that have to be returned
        TmpElectionObject[] memory obj = new TmpElectionObject[](_elections.length-1);

        // go through all elections
        for (uint i = 0; i < _elections.length; i++) {

            // save the election elements to the corresponding temporary object
            obj[i].id = _elections[i].electionId;
            obj[i].name = _elections[i].electionName;
            obj[i].startTimestamp = _elections[i].startTimestamp;
            obj[i].endTimestamp = _elections[i].endTimestamp;
            obj[i].votingSystem = _elections[i].votingSystem;

        }

        // return the temporary objects
        return obj;

    }

    // returns the electoral list for a given election
    function getElectoralList (uint256 electionId) public view returns (Candidate[] memory candidates) {

        for (uint256 i = 0; i < _elections.length; i++) {

            if (electionId == _elections[i].electionId) {
                return _elections[i].electoralList;
            }

        }

        return candidates;
    }

    function vote (uint256 electionId, Ballot memory ballot) public returns (bool) {
        if (!verifyElectionId(electionId)) {
            return false;
        }

        // Check if the address is allowed to vote
        uint256 iterator = 0;
        while (_elections[electionId].eligibleVoters[iterator] != msg.sender) {
            iterator++;
            if (iterator >= _elections[electionId].eligibleVoters.length) {
                return false;
            }
        }
        // Check if the address has already been used
        for (uint i = 0; i < _elections[electionId].usedAddresses.length; i++) {
            if (_elections[electionId].usedAddresses[i] == msg.sender) {
                return false;
            }
        }
        // Check the election time
        require(hasStarted(electionId), "Voting has not started yet");
        require(!isOver(electionId), "Voting already ended");

        // Add the ballot to the election
        _elections[electionId].ballots.push(ballot);

        // Store the vote
        if (_elections[electionId].votingSystem == VotingSystem.standardVoting) {
            _elections[electionId].votes[ballot.candidateId]++;
        } else {
            // Find prio 1 in ballot and store as vote
            for (uint i = 0; i < ballot.ranking.length; i++) {
                if (ballot.ranking[i] == 1) {
                    _elections[electionId].votes[i]++;
                }
            }
        } 

        // Remember the address has voted
        _elections[electionId].usedAddresses.push(msg.sender);

        return true;
    }

    function getVote (uint256 electionId) public view returns (string memory) {
        if (!verifyElectionId(electionId)) {
            return "Invalid Election ID";
        }

        // Check if the address has voted
        uint256 iterator = 0;
        while (_elections[electionId].usedAddresses[iterator] != msg.sender) {
            iterator++;
            if (iterator >= _elections[electionId].usedAddresses.length) {
                return "No vote submitted";
            }
        }
        // Find the ballot
        Ballot memory ballot;
        for (uint256 i = 0; i <= _elections[electionId].ballots.length; i++) {
            ballot = _elections[electionId].ballots[i];
            if (ballot.voterAddress == msg.sender) {
                break;
            }
        }
        // Return result based on voting system (standard or alternative)
        if (_elections[electionId].votingSystem == VotingSystem.standardVoting) {
            return string(abi.encode("You voted for ", _elections[electionId].electoralList[ballot.candidateId].firstName,
                " ", _elections[electionId].electoralList[ballot.candidateId].lastName));
        } else {
            return ""; // Not implemented yet
        }
    }

    function countVotes (uint256 electionId) public view returns (Candidate[] memory candidateRanking, uint256[] memory voteCount) {
        if (!verifyElectionId(electionId) || !hasStarted(electionId)) {
            return (candidateRanking, voteCount);
        }
        // Copy candidate array
    	Candidate[] memory electoralListCopy = _elections[electionId].electoralList;
        // Copy votes array
        uint256[] memory votesCopy = _elections[electionId].votes;
        // If election is not over or uses standard voting, "votes" array can be used
        if (!isOver(electionId) || (_elections[electionId].votingSystem == VotingSystem.standardVoting)) {
            // Quicksort implementation, stores candidate IDs in the order arrray
            sortVotes(votesCopy, electoralListCopy, int(0), int(votesCopy.length - 1));
            return (candidateRanking, voteCount);
        }
    }

    function isOver (uint256 electionId) private view returns (bool) {
        return (_elections[electionId].endTimestamp < block.timestamp);
    }

    function hasStarted (uint256 electionId) private view returns (bool) {
        return (_elections[electionId].startTimestamp < block.timestamp);
    }

    function verifyElectionId (uint256 electionId) private view returns (bool) {
        // Check if an election ID is valid
        for (uint256 i = 0; i < _elections.length; i++) {
            if (electionId == _elections[i].electionId) {
                return true;
            }
        }
        // Return false if ID has not been found
        return false;
    }
}