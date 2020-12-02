const BVS_backend = artifacts.require("bvs_backend");

contract("bvs_backend", (accounts) => {
  let instance;

  beforeEach(async () => {
    instance = await BVS_backend.deployed();
  });

  it("return empty string array on function call", async () => {
      await instance.createElection("0x3D350e8FAdDFE27C33CD16c03178326E82012b4c", 0, "test election", 12345, 12346);
      assert.equal(await instance.getElectionInformation().length, 1, "strings should be emtpy");
  });

  it("return id 0", async () => {
    await instance.createElection("0x3D350e8FAdDFE27C33CD16c03178326E82012b4c", 0, "test election", 12345, 12346);
    assert.equal(await instance.getLastElectionId(), 0, "election id should be 0");
});

});
