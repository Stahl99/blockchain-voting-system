# How to use the Blockchain Admintool

* Go to https://github.com/Stahl99/blockchain-voting-system/releases/ to get the latest release of the client side applications
* Download and extract the .zip files
* To run the admintool simply start the Blockchain-Admintool.exe
* Make sure your local chain is up and running.
* Enter your election specification like start and end date, candidates, amount of voters, ...
* Enter your blockchain url, the public key of a wallet with enough ether (f.e. an account created by truffle) and the contract address the bvs_backend contract is runnning at. 
  You can get this information from the `npm run migrate` step [see here](../backend/)
* Click the save button to write the election to the blockchain
* Choose a file location for the generated private keys (stored in PrKeys.txt)
* Use these private keys to vote in the client tool
