pragma solidity ^0.5.17;

contract DHBWCoin {
    mapping(address => uint256) private _balances;
    mapping(address => mapping(address => uint256)) private _allowances;
    uint256 private _totalSupply;

    event Transfer(
        address indexed from,
        address indexed recipient,
        uint256 amount
    );
    event Approval(
        address indexed owner,
        address indexed spender,
        uint256 amount
    );

    constructor() public {
        _totalSupply = 10000;
        _balances[msg.sender] = _totalSupply;
    }

    function name() public pure returns (string memory) {
        return "DHBWCoin";
    }

    function symbol() public pure returns (string memory) {
        return "DHBW";
    }

    function decimals() public pure returns (uint8) {
        return 2;
    }

    function totalSupply() public view returns (uint256) {
        return _totalSupply;
    }

    function balanceOf(address account) public view returns (uint256) {
        return _balances[account];
    }

    function transfer(address recipient, uint256 amount) public returns (bool) {
        require(
            _balances[msg.sender] - amount >= 0,
            "Sender does not have enough coins"
        );
        _balances[msg.sender] -= amount;
        _balances[recipient] += amount;
        emit Transfer(msg.sender, recipient, amount);
        return true;
    }

    function approve(address spender, uint256 amount) public returns (bool) {
        _allowances[msg.sender][spender] = amount;
        emit Approval(msg.sender, spender, amount);
        return true;
    }

    function allowance(address owner, address spender)
        public
        view
        returns (uint256)
    {
        return _allowances[owner][spender];
    }

    function transferFrom(
        address from,
        address recipient,
        uint256 amount
    ) public returns (bool) {
        require(
            _allowances[from][msg.sender] >= amount,
            "Allowance does not suffice"
        );
        require(
            _balances[from] - amount >= 0,
            "Sender does not have enough coins"
        );
        _balances[from] -= amount;
        _balances[recipient] += amount;
        _allowances[from][msg.sender] -= amount;
        emit Transfer(from, recipient, amount);

        return true;
    }
}
