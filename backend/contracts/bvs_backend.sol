pragma solidity ^0.7.2;

contract bvs_backend {

    struct Candidate {
        string firstName;
        string lastName;
        string party;
        int id;
    }

    enum VotingSystem { standardVoting, alternativeVoting }

    struct Ballot {
        // standardVoting 
        int candidateId;

        // alternativeVoting
        int[] ranking; // by candidate id
    }

    struct Election {
        int electionId;
    
        Candidate[] electoralList; 
        Ballot[] ballots;
        address[] eligibleVoters;
        address[] usedAddresses;
        VotingSystem votingSystem;

        uint256 startTime;
        uint256 endTime;
    }

   Election[] private _elections;

    constructor() {}



}