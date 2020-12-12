/* SPDX-License-Identifier: UNLICENSED */
pragma solidity ^0.7.2;
pragma experimental ABIEncoderV2;

/**
* @notice Quicksort implementation to sort votes and candidates array in ascending order
* @param votes The votes array to be sorted
* @param candidates The candidates array, it will be sorted the same way as votes array
* @param left Left side index of the list for recursive call
* @param right Right side index of the list for recursive call
*/
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

/// @title Blockchain Voting System (BVS) backend
/// @author K. Radke, L. Neuffer, L. Seyboldt, S. Stahl
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
        bool finalResult;
        Candidate[] candidates;
        uint256[] votes;
    }

    Election[] private _elections; // array of all elections
    Election temp; // temporary election storage
    Candidate tempCandidate;
    Ballot tempBallot;

    event Vote(address sender, Ballot _ballot);

    constructor() {}

    /// @notice creates a new election
    /// @param electionAdminAddress Wallet address of the admin account
    /// @param electionVotingSystem Voting system of the election
    /// @param electionName Description / name of the election
    /// @param electionStartTimestamp Unix timestamp of the beginning of the election
    /// @param electionEndTimestamp Unix timestamp of the end of the election
    function createElection (address electionAdminAddress, VotingSystem electionVotingSystem, string memory electionName,
    uint256 electionStartTimestamp, uint256 electionEndTimestamp) public {
        
        // set values in temporary election
        temp.electionId = _elections.length; // set and increment id 
        temp.electionName = electionName;
        temp.votingSystem = electionVotingSystem;
        temp.adminAddress = electionAdminAddress;
        temp.startTimestamp = electionStartTimestamp;
        temp.endTimestamp = electionEndTimestamp;
        temp.result.finalResult = false; // No result has been calculated

        _elections.push(temp); // save the new election on the blockchain

    }

    /// @notice Returns the election id of the last created election
    /// @return Election id of the last created election
    function getLastElectionId () public view returns (uint256) {
        return _elections.length-1;
    }

    /// @notice Replaces the list of the current eligible voters for a given election
    /// @param electionId Id of the election whose eligible voters list is to be replaced
    /// @param newEligibleVoterList List of addresses of the new eligible voters
    /// @return Returns true if successfull; false otherwise
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

    /// @notice Replaces the list of the current electoral list for a given election
    /// @param electionId Id of the election whose electoral list is to be replaced<
    /// @param newElectoralList List of candidates for the election
    /// @return Returns true if successfull; false otherwise

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

    /// @notice Adds a candidate to the electoral list for a given election
    /// @param electionId Id of the election to which the new candidate is to be added
    /// @param newCandidate Data of the new candidate
    function addCandidate (uint256 electionId, Candidate memory newCandidate) public {

        for (uint i = 0; i < _elections.length; i++) {

            // find the correct election
            if (_elections[i].electionId == electionId) {
                _elections[i].electoralList.push(newCandidate);
                _elections[i].votes.push(0);
            }

        }
    }

    /// @notice Returns the ids, names, start- and end-timestamps of all elections in a temporary election object. This is done so that the C# code can be generated better.
    /// @return Returns the ids, names, start- and end-timestamps of all elections in a temporary election object
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

    /// @notice Returns the electoral list for the given election id
    /// @param electionId Id of the election of which the electoral list is to be returned
    /// @return candidates returns the electoral list for the given election id
    function getElectoralList (uint256 electionId) public view returns (Candidate[] memory candidates) {

        for (uint i = 0; i < _elections.length; i++) {

            if (electionId == _elections[i].electionId) {
                return _elections[i].electoralList;
            }

        }

        return candidates;
    }

    /**
    * @notice Function used for voting. Checks if voter is eligible, has not already voted and is inside the timeframe.
    * @param electionId ID of the election the ballot is for
    * @param ballot Complete ballot with with candidate ID or ranking set
    * @return true on success
    */
    function vote (uint256 electionId, Ballot memory ballot) public returns (bool) {
        if (!verifyElectionId(electionId)) {
            revert("Invalid election ID");
        }

        // Check if the address is allowed to vote
        for (uint256 i = 0; i < _elections[electionId].eligibleVoters.length; i++) {
            if (_elections[electionId].eligibleVoters[i] == msg.sender) {
                break;
            }

            if (i == _elections[electionId].eligibleVoters.length - 1) {
                revert("Address is not an eligible voter");
            }
        }
    
        // Check if the address has already been used
        for (uint i = 0; i < _elections[electionId].usedAddresses.length; i++) {
            if (_elections[electionId].usedAddresses[i] == msg.sender) {
                revert("Address has already voted");
            }
        }
        
        // Check the election time
        if (!hasStarted(electionId) || isOver(electionId)) {
            revert("Out of election timeframe");
        } 
        
        ballot.voterAddress = msg.sender;
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

    /**
    * @notice Returns the ballot submitted from the sender address
    * @param electionId ID of the election that should contain the ballot
    * @return Submitted ballot if found, empty ballot if not found
    */
    function getVoteBallot (uint256 electionId) public view returns (Ballot memory) {

        Ballot memory ballot;
        ballot.voterAddress = address(0);
        ballot.candidateId = 0;

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

    /**
    * @notice Counts votes at the current state of the election and stores the result
    * @param electionId ID of the election that should be counted
    */
    function countVotes (uint256 electionId) public {
        if (!verifyElectionId(electionId)) {
            revert();
        }

        // If final result is stored, no calculation needed
        if (_elections[electionId].result.finalResult) {
            return;
        }

        /*** Calculate result: ***/

        // Copy candidate array
    	Candidate[] memory cands = _elections[electionId].electoralList;
        // Copy votes array
        uint256[] memory votes = _elections[electionId].votes;
        // Copy ballots array
        Ballot[] memory ballots = _elections[electionId].ballots;

        // Catch empty ballots array
        if (ballots.length == 0) {
            return;
        }

        // Variables for calculation
        uint lastCandidateId;
        uint lastCandidateIndex = 0;
        
        // If election is not over or uses standard voting, "votes" array can be used
        if (_elections[electionId].votingSystem == VotingSystem.standardVoting) {
            // Quicksort implementation, stores candidate IDs in the order arrray
            sortVotes(votes, cands, int(0), int(votes.length - 1));
        }

        // Election is over and uses alternative voting
        else if (_elections[electionId].votingSystem == VotingSystem.alternativeVoting) {
            sortVotes(votes, cands, int(0), int(votes.length - 1));
            // While there is no winner (>50% of votes), remove last candidate
            // and reassign the second prio votes
            while (votes[votes.length - 1] <= ballots.length / 2) {
                // If only two candidates are left the result is a draw
                if (lastCandidateIndex == votes.length - 2) {
                    break;
                }
                // Determine last candidate and set his votes to 0
                lastCandidateId = cands[lastCandidateIndex].id;
                votes[lastCandidateId] = 0;
                // Look for ballots with the last candidate as prio 1
                for (uint256 i = 0; i < ballots.length; i++) {
                    if (ballots[i].ranking.length > 0 &&
                        ballots[i].ranking[lastCandidateId] == 1) {
                            // Look for the candidate with prio 2 (candidate id = j)
                            for (uint j = 0; j < ballots[i].ranking.length; j++) {
                                if (ballots[i].ranking[j] == 2) {
                                    // Add second prio to according candidate in votes array
                                    for (uint k = 0; k < cands.length; k++) {
                                        if (cands[k].id == j) {
                                            votes[k]++;
                                        }
                                    }
                                }
                                // Reduce prios by one, unused prio 1 turns 0
                                ballots[i].ranking[j]--;
                            }
                        }
                }
                // Sort again with secondary votes included
                sortVotes(votes, cands, int(0), int(votes.length - 1));
                // Increment to eliminate last candidate in next iteration
                lastCandidateIndex++;
            }
        }

        delete _elections[electionId].result.votes;
        delete _elections[electionId].result.candidates;

        // Store the calculated result
        for (uint i = 0; i < votes.length; i++) {
            _elections[electionId].result.votes.push(votes[i]);
            _elections[electionId].result.candidates.push(cands[i]);
            _elections[electionId].result.finalResult = (isOver(electionId) || (_elections[electionId].usedAddresses.length == _elections[electionId].eligibleVoters.length));
        }
    }

    /**
    * @notice Returns the stored result
    * @param electionId ID of the requested election
    * @return votes array contains the amount of votes, candidates is a sorted electoral list
    */
    function getResult (uint electionId) public view returns (uint256[] memory votes, Candidate[] memory candidates) {
        //require(!_elections[electionId].result.empty, "No result calculated, call countVotes first");
        return (_elections[electionId].result.votes,
                _elections[electionId].result.candidates);
    }

    /**
    * @notice Used to determine if an election is over
    * @param electionId ID of the election
    * @return Returns true if the election timeframe is over
    */
    function isOver (uint256 electionId) private view returns (bool) {
        return (_elections[electionId].endTimestamp < block.timestamp);
    }

    /**
    * @notice Used to determine if an election has started
    * @param electionId ID of the election
    * @return Returns true if the election timeframe has started
    */
    function hasStarted (uint256 electionId) private view returns (bool) {
        return (_elections[electionId].startTimestamp < block.timestamp);
    }

    /**
    * @notice Used to check if an election ID is valid
    * @param electionId ID of the election to check
    * @return Returns true if the election can be found
    */
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