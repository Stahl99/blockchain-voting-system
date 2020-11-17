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

    Candidate[] private _electoralList; 
    Ballot[] private _ballots;
    address[] private _eligibleVoters;
    address[] private _usedAddresses;
    VotingSystem private _votingSystem;

    constructor() {}



}