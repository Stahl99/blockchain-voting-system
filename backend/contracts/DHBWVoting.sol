pragma solidity ^0.7.2;

contract DHBWVoting {
    // Mapping that stores the votes
    mapping(string => uint256) private _votes;
    // Mapping that stores if someone has voted
    mapping(address => bool) private _voted;
    // Array which stores all voted options with at least one vote
    string[] private _votingOptions;

    // Event fired when someone has voted
    event Vote(address indexed _voter, string _votingOption);

    constructor() {}

    // Gets the currently winning option(s)
    function getWinners() public view returns (string memory) {
        string[] memory winner = new string[](0);
        uint256 winnerAmount = 0;
        // Search in votes mapping for most votes
        for (uint256 i = 0; i < _votingOptions.length; i++) {
            // If more votes found, set a new winner array and amount
            if (winnerAmount < _votes[_votingOptions[i]]) {
                winner = new string[](1);
                winner[0] = _votingOptions[i];
                winnerAmount = _votes[winner[0]];
            // If equal votes found, append the option to winner array
            } else if (winnerAmount == _votes[_votingOptions[i]]) {
                string[] memory tempWinner = winner;
                winner = new string[](winner.length + 1);
                for (uint256 j = 0; j < tempWinner.length; j++) {
                    winner[j] = tempWinner[j];
                }
                winner[tempWinner.length] = _votingOptions[i];
            }
        }
        // As it is currently experimental to return string arrays, this serializes
        // the array to a JSON string
        string memory serializedWinner = "[";
        for (uint256 i = 0; i < winner.length; i++) {
            serializedWinner = concatString(serializedWinner, '"', winner[i], '"');
            if (i != winner.length - 1) {
                serializedWinner = concatString(serializedWinner, ",");
            }
        }
        serializedWinner = concatString(serializedWinner, "]");
        return serializedWinner;
    }

    // Gets vote amount for a given option
    function getVoteAmount(string memory votingOption) public view returns (uint256) {
        return _votes[votingOption];
    }

    // Returns whether an account has already voted
    function hasVoted(address account) public view returns (bool) {
        return _voted[account];
    }

    // Registers a vote
    function vote(string memory votingOption) public returns (bool) {
        // Check if someone already has voted
        require(_voted[msg.sender] == false, "Address has already voted");
        // If this option wasn't voted for before, add it to the array
        if (_votes[votingOption] == 0) {
            _votingOptions.push(votingOption);
        }
        // Add a vote
        _votes[votingOption] += 1;
        // Set the address to have voted
        _voted[msg.sender] = true;
        // Emit voted event
        emit Vote(msg.sender, votingOption);
        return true;
    }

    function concatString(string memory a, string memory b) internal pure returns (string memory) {
        return string(abi.encodePacked(a, b));
    }

    function concatString(string memory a, string memory b, string memory c, string memory d) internal pure returns (string memory) {
        return string(abi.encodePacked(a, b, c, d));
    }

    function multiply(uint256 a, uint256 b) public pure returns (uint256) {
        return a*b;
    }
}
