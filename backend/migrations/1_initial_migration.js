const Migrations = artifacts.require("Migrations");
const DHBWCoin = artifacts.require("DHBWCoin");
const DHBWVoting = artifacts.require("DHBWVoting");
const bvs_backend = artifacts.require("bvs_backend")

module.exports = function (deployer) {
  deployer.deploy(Migrations);
  deployer.deploy(DHBWCoin);
  deployer.deploy(DHBWVoting);
  deployer.deploy(bvs_backend);
};
