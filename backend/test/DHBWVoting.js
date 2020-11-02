const DHBWVoting = artifacts.require("DHBWVoting");

contract("DHBWVoting", (accounts) => {
  const alice = accounts[0];
  const bob = accounts[1];
  const carol = accounts[2];
  const daniel = accounts[3];

  let instance;

  beforeEach(async () => {
    instance = await DHBWVoting.deployed();
  });

  it("should deploy and have no winner", async () => {
    assert.equal(await instance.getWinners(), "[]", "winner should be empty");
  });

  it("should update votes on voting", async () => {
    const tx = await instance.vote("test");
    assert.equal(tx.receipt.status, true, "voting should be possible");
    assert.equal(
      await instance.getVoteAmount("test"),
      1,
      "vote amount should be 1"
    );
  });

  it("should disallow multiple votes per address", async () => {
    try {
      await instance.vote("test");
    } catch (e) {
      return true;
    }
    return false;
  });

  it("should correctly determine a winner", async () => {
    await instance.vote("test2", { from: bob });
    await instance.vote("test2", { from: carol });
    assert.equal(
      await instance.getWinners(),
      '["test2"]',
      "winner should be test2"
    );
    assert.equal(
      await instance.getVoteAmount("test2"),
      2,
      "voteAmount of test2 should be 2"
    );
  });

  it("should correctly determine multiple winners", async () => {
    await instance.vote("test", { from: daniel });
    const winners = JSON.parse(await instance.getWinners());
    assert.equal(winners.length, 2, "there should be 2 winners");
    assert.notEqual(
      winners.findIndex((value) => value === "test"),
      -1,
      "test should be a winner"
    );
    assert.notEqual(
      winners.findIndex((value) => value === "test2"),
      -1,
      "test2 should be a winner"
    );
  });
});
