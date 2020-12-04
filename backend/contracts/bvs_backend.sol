/* SPDX-License-Identifier: UNLICENSED */
pragma solidity ^0.7.2;
pragma experimental ABIEncoderV2;

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

        Result result; // Stores the result

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

    struct Result {
        bool empty;
        Candidate[] candidates;
        uint256[] votes;
    }

    Election[] private _elections; // array of all elections
    Election temp; // temporary election storage
    Candidate tempCandidate;
    Ballot tempBallot;

    event Vote(address sender, Ballot _ballot);

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
                        tempCandidate.firstName = newElectoralList[j].firstName;
                        tempCandidate.lastName = newElectoralList[j].lastName;
                        tempCandidate.party = newElectoralList[j].party;
                        tempCandidate.id = newElectoralList[j].id;

                        _elections[i].electoralList.push(tempCandidate);
                        _elections[i].votes.push(0);
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
        TmpElectionObject[] memory obj = new TmpElectionObject[](_elections.length);

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

    function getElectionStrings () public view returns (string[] memory) {
        string[] memory obj  = new string[](_elections.length);

        for (uint i = 0; i < _elections.length; i++) {

            // save the election elements to the corresponding temporary object
            obj[i] = _elections[i].electionName;
        }

        return obj;
    }

    // returns the electoral list for a given election
    function getElectoralList (uint256 electionId) public view returns (Candidate[] memory candidates) {

        for (uint i = 0; i < _elections.length; i++) {

            if (electionId == _elections[i].electionId) {
                return _elections[i].electoralList;
            }

        }

        return candidates;
    }

    function vote (uint256 electionId, Ballot memory ballot) public returns (bool) {
        if (!verifyElectionId(electionId)) {
            revert();
        }

        // Check if the address is allowed to vote
        for (uint256 i = 0; i < _elections[electionId].eligibleVoters.length; i++) {
            if (_elections[electionId].eligibleVoters[i] == msg.sender) {
                break;
            }

            if (i == _elections[electionId].eligibleVoters.length - 1) {
                revert();
            }
        }
    
        // Check if the address has already been used
        for (uint i = 0; i < _elections[electionId].usedAddresses.length; i++) {
            if (_elections[electionId].usedAddresses[i] == msg.sender) {
                revert();
            }
        }
        
        // Check the election time
        if (!hasStarted(electionId) || isOver(electionId)) {
            revert();
        } 
        
        ballot.voterAddress = msg.sender;
        _elections[electionId].ballots.push(ballot);

        
        // Store the vote
        if (_elections[electionId].votingSystem == VotingSystem.standardVoting) {
            _elections[electionId].votes[tempBallot.candidateId]++;
        } else {
            // Find prio 1 in ballot and store as vote
            for (uint i = 0; i < tempBallot.ranking.length; i++) {
                if (tempBallot.ranking[i] == 1) {
                    _elections[electionId].votes[i]++;
                }
            }
        }   

        // Remember the address has voted
        _elections[electionId].usedAddresses.push(msg.sender);

        return true;
    }

    function getVoteString (uint256 electionId, address requestAddress) public view returns (string memory) {
        if (!verifyElectionId(electionId)) {
            return "Invalid Election ID";
        } 

        // Check if the address has voted
        address[] memory usedAddressesCpy = _elections[electionId].usedAddresses;
        for (uint256 i = 0; i < usedAddressesCpy.length; i++) {
            if (usedAddressesCpy[i] == requestAddress) {
                break;
            }
            if (i == usedAddressesCpy.length - 1) {
                return "No vote submitted";
            }
        }        

        // Find the ballot
        Ballot memory ballot;
        for (uint256 i = 0; i < _elections[electionId].ballots.length; i++) {
            ballot = _elections[electionId].ballots[i];
            if (ballot.voterAddress == requestAddress) {
                break;
            }
        }

        // Return result based on voting system (standard or alternative)
        if (_elections[electionId].votingSystem == VotingSystem.standardVoting) {
            return "not implemented yet";
        } else {
            return "not implemented yet";
        } 
    }

    // Returns the ballot submitted from the sender address
    // Return ballot is empty if nothing was found
    function getVoteBallot (uint256 electionId) public view returns (Ballot memory) {

        Ballot memory ballot;
        ballot.voterAddress = address(0);
        ballot.candidateId = block.timestamp;// return length of ballot array for debug

        if (!verifyElectionId(electionId)) {
            return ballot;
        } 

        // Check if the address has voted
        address[] memory usedAddressesCpy = _elections[electionId].usedAddresses;
        for (uint256 i = 0; i < usedAddressesCpy.length; i++) {
            if (usedAddressesCpy[i] == msg.sender) {
                break;
            }
            if (i == usedAddressesCpy.length - 1) {
                return ballot;
            }
        }        

        // Find the ballot
        for (uint256 i = 0; i < _elections[electionId].ballots.length; i++) {
            ballot = _elections[electionId].ballots[i];
            //ballot.candidateId = 10;
            if (ballot.voterAddress == msg.sender) {
                break;
            }
        }

        return ballot;
    }

    function countVotes (uint256 electionId) public returns (Candidate[] memory candidateRanking, uint256[] memory voteCount) {
        if (!verifyElectionId(electionId) || !hasStarted(electionId)) {
            return (candidateRanking, voteCount);
        }

        // If a result is already stored, return it
        if (_elections[electionId].result.empty == false) {
            return (_elections[electionId].result.candidates, _elections[electionId].result.votes);
        }

        // Calculate result:

        // Copy candidate array
    	Candidate[] memory cands = _elections[electionId].electoralList;
        // Copy votes array
        uint256[] memory votes = _elections[electionId].votes;
        // Variables for calculation
        uint lastCandidateId;
        uint lastCandidateIndex = 0;
        
        // If election is not over or uses standard voting, "votes" array can be used
        if (!isOver(electionId) || (_elections[electionId].votingSystem == VotingSystem.standardVoting)) {
            // Quicksort implementation, stores candidate IDs in the order arrray
            sortVotes(votes, cands, int(0), int(votes.length - 1));
        }

        // Election is over and uses alternative voting
        else if (_elections[electionId].votingSystem == VotingSystem.alternativeVoting) {
            sortVotes(votes, cands, int(0), int(votes.length - 1));
            // While there is no winner (>50% of votes), remove last candidate
            // and reassign the second prio votes
            while (votes[votes.length-1] <= _elections[electionId].ballots.length) {
                lastCandidateId = cands[lastCandidateIndex].id;
                // Look for ballots with the last candidate as prio 1
                for (uint256 i = 0; i < _elections[electionId].ballots.length; i++) {
                    if (_elections[electionId].ballots[i].ranking.length > 0 &&
                        _elections[electionId].ballots[i].ranking[lastCandidateId] == 1) {
                            // Look for the candidate with prio 2 (candidate id = j)
                            for (uint j = 0; j < cands.length; j++) {
                                if (_elections[electionId].ballots[i].ranking[j] == lastCandidateIndex + 2) {
                                    // Add second prio to according candidate in votes array
                                    for (uint k = 0; k < cands.length; k++) {
                                        if (cands[k].id == j) {
                                            votes[k]++;
                                        }
                                    }
                                }
                            }
                        }
                }
                // Sort again with secondary votes included
                sortVotes(votes, cands, int(0), int(votes.length - 1));
                // Increment to eliminate last candidate in next iteration
                lastCandidateIndex++;
            }
        }

        // If election is over, store the calculated result
        if (isOver(electionId)) {
            for (uint i = 0; i < votes.length; i++) {
                _elections[electionId].result.votes.push(votes[i]);
                _elections[electionId].result.candidates.push(cands[i]);
            }
        }

        return (cands, votes);
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