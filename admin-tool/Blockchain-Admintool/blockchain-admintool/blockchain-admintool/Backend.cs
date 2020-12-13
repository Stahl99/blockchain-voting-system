using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Nethereum.Web3.Accounts;
using Nethereum.HdWallet;
using BlockchainVotingSystem.Contracts.DHBWVoting;
using BlockchainVotingSystem.Contracts.bvs_backend;
using BlockchainVotingSystem.Contracts.bvs_backend.ContractDefinition;
using Nethereum.Web3;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Web3.Accounts.Managed;
using System.IO;

namespace blockchain_admintool
{
    public class Backend
    {

        Web3 web3;
        Bvs_backendService votingService;
        GetElectionInformationOutputDTO allElectionInfo;
        TmpElectionObject currentElection;
        GetElectoralListOutputDTO backendCandidates;

        /// <summary>
        /// creates election with given admin address and time stamps and wirtes on blockchain with CreateElection()
        /// adds given candidates to blockchain with AddCandidate()
        /// creates new voter accounts with public and private keys
        /// transfers enough ether to voter accounts to run vote-function on blockchain
        /// writes public keys of voter accounts in eligible voter list of the created election with ReplaceListOfEligibleVoters()
        /// </summary>
        /// <param name="admAdr"></param>
        /// <param name="votingSys"></param>
        /// <param name="candList"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="description"></param>
        /// <param name="voterCount"></param>
        /// <param name="prKeyLoc"></param>
        public async void CreateElection(string admAdr, int votingSys, List<Candidate> candList, DateTime start, DateTime stop, string description, int voterCount, string prKeyLoc)
        {

            
            await votingService.CreateElectionRequestAsync(admAdr, (byte)votingSys, description, DateToUnix(start), DateToUnix(stop));

            System.Numerics.BigInteger id = votingService.GetLastElectionIdQueryAsync().Result;

            foreach(Candidate c in candList)
            {
                await votingService.AddCandidateRequestAsync(id, c);
            }

            List<String> voters = new List<string>();

            List<String> privateKeys = new List<string>();

            for (int i = 0; i < voterCount; i++)
            {
                var ecKey = Nethereum.Signer.EthECKey.GenerateKey();
                var privateKey = ecKey.GetPrivateKeyAsBytes().ToHex();
                var account = new Account(privateKey);

                voters.Add(account.Address.ToString());
                privateKeys.Add(privateKey.ToString());

                await web3.Eth.GetEtherTransferService().TransferEtherAndWaitForReceiptAsync(account.Address, 3.3m);
            }

            await votingService.ReplaceListOfEligibleVotersRequestAsync(id, voters);

            PrintPrKeys(privateKeys, prKeyLoc);

            MessageBox.Show("Wahl erfolgreich erstellt");

        }

        /// <summary>
        /// converts .net datetime objects to unix time stamps
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public System.Numerics.BigInteger DateToUnix(DateTime d)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            Int32 unixdt = (Int32)(d.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            System.Numerics.BigInteger tmp = (System.Numerics.BigInteger)unixdt;

            return unixdt;
        }

        /// <summary>
        /// set contract address from user input
        /// </summary>
        /// <param name="contractAdress"></param>
        /// <returns></returns>

        public bool SetContractAdress(String contractAdress)
        {
            // check format 
            if (!OnlyHexInString(contractAdress))
            {
                MessageBox.Show("Wrong format for election adresss (must be hex-string)");
                return false;
            }

            // create voting service with new contract adress
            this.votingService = new Bvs_backendService(web3, contractAdress);
            return true;
        }

        /// <summary>
        /// Takes a Strinig-list with private keys and prints them into a .txt file in given path
        /// </summary>
        /// <param name="prKeys"></param>
        /// <param name="path"></param>

        public void PrintPrKeys(List<string> prKeys, string path)
        {

            using var sw = new StreamWriter(path + "\\PrivKeys.txt");
            
            foreach(String s in prKeys)
            {
                sw.WriteLine(s);
            }

            sw.Close();

        }
        /// <summary>
        /// Initalizes Web3-object with given blockchain URL and given Admin-wallet
        /// </summary>
        /// <param name="url"></param>
        /// <param name="walletAddr"></param>
        public void SetBlockchainUrl(String url, String walletAddr)
        {
            if (url.Length != 0)
            {

                /*
                var privateKey = "0x5569b93622765c3100095da4d24e9494231dc01873ad7c07d69acc06cc1ca3b3";
                var account = new Account(privateKey);*/

                var account = new ManagedAccount(walletAddr, "test");


               
                this.web3 = new Web3(account, url);

                
            
            }
        }

        /// <summary>
        /// Init the contract service with a blockchain url, admin wallet address and contractAdress
        /// </summary>
        /// <param name="url"></param>
        /// <param name="contractAdress"></param>
        /// <param name="walletAddr"></param>
        /// <returns></returns>
        public bool InitService(String url, String contractAdress, string walletAddr)
        {
            // The order of these functions is important! Web3 object is needed for service
            SetBlockchainUrl(url, walletAddr);
            if (!SetContractAdress(contractAdress))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// checks if string is in hex format
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        public bool OnlyHexInString(string test)
        {
            // For C-style hex notation (0xFF) you can use @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z"
            return System.Text.RegularExpressions.Regex.IsMatch(test, @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z");
        }

    }
}
