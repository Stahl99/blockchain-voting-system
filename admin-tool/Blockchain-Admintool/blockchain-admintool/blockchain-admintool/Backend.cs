﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using BlockchainVotingSystem.Contracts.DHBWVoting;
using BlockchainVotingSystem.Contracts.bvs_backend;
using BlockchainVotingSystem.Contracts.bvs_backend.ContractDefinition;
using Nethereum.Web3;

namespace blockchain_admintool
{
    public class Backend
    {

        Web3 web3;
        Bvs_backendService votingService;
        GetElectionInformationOutputDTO allElectionInfo;
        TmpElectionObject currentElection;
        GetElectoralListOutputDTO backendCandidates;

        public void CreateElection(string admAdr, int votingSys, List<Candidate> candList, DateTime start, DateTime stop, string description)
        {

            
            string id = votingService.CreateElectionRequestAsync(admAdr, (byte)votingSys, description, DateToUnix(start), DateToUnix(stop)).Result;

            string id_1 = votingService.ReplaceElectoralListRequestAsync((System.Numerics.BigInteger)(Convert.ToInt32(id)), candList).Result;

            if(String.Compare(id, id_1) != 0)
            {
                MessageBox.Show("das sollte so nicht sein");
            }

        }

        public System.Numerics.BigInteger DateToUnix(DateTime d)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            Int32 unixdt = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            MessageBox.Show(unixdt.ToString());

            System.Numerics.BigInteger tmp = (System.Numerics.BigInteger)unixdt;

            MessageBox.Show(unixdt.ToString());

            return unixdt;
        }


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

        public void SetBlockchainUrl(String url)
        {
            if (url.Length != 0)
            {

                MessageBox.Show(url);
                this.web3 = new Web3(url);
            }
        }

        // Init the contract service with a blockchain url and contractAdress
        public bool InitService(String url, String contractAdress)
        {
            // The order of these functions is important! Web3 object is needed for service
            SetBlockchainUrl(url);
            if (!SetContractAdress(contractAdress))
            {
                return false;
            }

            return true;
        }

        private bool OnlyHexInString(string test)
        {
            // For C-style hex notation (0xFF) you can use @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z"
            return System.Text.RegularExpressions.Regex.IsMatch(test, @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z");
        }

    }
}
