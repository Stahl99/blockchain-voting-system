const Migrations = artifacts.require("Migrations");
const DHBWCoin = artifacts.require("DHBWCoin");
const DHBWVoting = artifacts.require("DHBWVoting");

module.exports = function (deployer) {
  deployer.deploy(Migrations);
  deployer.deploy(DHBWCoin);
  deployer.deploy(DHBWVoting);
};
