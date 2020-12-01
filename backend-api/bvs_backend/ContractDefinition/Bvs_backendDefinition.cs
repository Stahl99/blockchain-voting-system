using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts;
using System.Threading;

namespace BlockchainVotingSystem.Contracts.bvs_backend.ContractDefinition
{


    public partial class Bvs_backendDeployment : Bvs_backendDeploymentBase
    {
        public Bvs_backendDeployment() : base(BYTECODE) { }
        public Bvs_backendDeployment(string byteCode) : base(byteCode) { }
    }

    public class Bvs_backendDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "608060405234801561001057600080fd5b506124c8806100206000396000f3fe608060405234801561001057600080fd5b506004361061009e5760003560e01c8063c7ec836711610066578063c7ec836714610142578063da2b86d114610157578063ef8e51e61461016a578063f35953cc1461017f578063f7f09547146101945761009e565b80631840f0ca146100a35780632df0f580146100cd5780634aea2f6a146100ed5780635a55c1f01461010d578063934422a91461012d575b600080fd5b6100b66100b1366004611da2565b6101a7565b6040516100c4929190612263565b60405180910390f35b6100e06100db366004611dba565b6104ec565b6040516100c49190612349565b6101006100fb366004611da2565b6105ba565b6040516100c49190612249565b61012061011b366004611da2565b610847565b6040516100c49190612354565b610135610b35565b6040516100c491906121e9565b61014a610c73565b6040516100c4919061243b565b6100e0610165366004611f82565b610c7d565b610172610fc7565b6040516100c491906122ac565b61019261018d366004611d2f565b611216565b005b6100e06101a2366004611e62565b6113d4565b6060806101b383611592565b15806101c557506101c3836115e1565b155b156101cf576104e7565b6060600084815481106101de57fe5b90600052602060002090600a0201600201805480602002602001604051908101604052809291908181526020016000905b8282101561040e5760008481526020908190206040805160048602909201805460026001821615610100026000190190911604601f8101859004909402830160a090810190925260808301848152929390928492909184918401828280156102b85780601f1061028d576101008083540402835291602001916102b8565b820191906000526020600020905b81548152906001019060200180831161029b57829003601f168201915b50505050508152602001600182018054600181600116156101000203166002900480601f01602080910402602001604051908101604052809291908181526020018280546001816001161561010002031660029004801561035a5780601f1061032f5761010080835404028352916020019161035a565b820191906000526020600020905b81548152906001019060200180831161033d57829003601f168201915b5050509183525050600282810180546040805160206001841615610100026000190190931694909404601f810183900483028501830190915280845293810193908301828280156103ec5780601f106103c1576101008083540402835291602001916103ec565b820191906000526020600020905b8154815290600101906020018083116103cf57829003601f168201915b505050505081526020016003820154815250508152602001906001019061020f565b50505050905060606000858154811061042357fe5b90600052602060002090600a020160030180548060200260200160405190810160405280929190818152602001828054801561047e57602002820191906000526020600020905b81548152602001906001019080831161046a575b5050505050905061048e8561160b565b15806104c7575060008086815481106104a357fe5b600091825260209091206007600a90920201015460ff1660018111156104c557fe5b145b156104e4576104dd818360006001855103611635565b50506104e7565b50505b915091565b6000805b6000548110156105ae57836000828154811061050857fe5b90600052602060002090600a02016000015414156105a657336001600160a01b03166000828154811061053757fe5b60009182526020909120600a909102016007015461010090046001600160a01b0316141561059c57826000828154811061056d57fe5b90600052602060002090600a020160050190805190602001906105919291906117b5565b5060019150506105b4565b60009150506105b4565b6001016104f0565b50600090505b92915050565b606060005b60005481101561084057600081815481106105d657fe5b90600052602060002090600a02016000015483141561083857600081815481106105fc57fe5b90600052602060002090600a0201600201805480602002602001604051908101604052809291908181526020016000905b8282101561082c5760008481526020908190206040805160048602909201805460026001821615610100026000190190911604601f8101859004909402830160a090810190925260808301848152929390928492909184918401828280156106d65780601f106106ab576101008083540402835291602001916106d6565b820191906000526020600020905b8154815290600101906020018083116106b957829003601f168201915b50505050508152602001600182018054600181600116156101000203166002900480601f0160208091040260200160405190810160405280929190818152602001828054600181600116156101000203166002900480156107785780601f1061074d57610100808354040283529160200191610778565b820191906000526020600020905b81548152906001019060200180831161075b57829003601f168201915b5050509183525050600282810180546040805160206001841615610100026000190190931694909404601f8101839004830285018301909152808452938101939083018282801561080a5780601f106107df5761010080835404028352916020019161080a565b820191906000526020600020905b8154815290600101906020018083116107ed57829003601f168201915b505050505081526020016003820154815250508152602001906001019061062d565b50505050915050610842565b6001016105bf565b505b919050565b606061085282611592565b6108865750604080518082019091526013815272125b9d985b1a5908115b1958dd1a5bdb881251606a1b6020820152610842565b60005b336001600160a01b0316600084815481106108a057fe5b90600052602060002090600a020160060182815481106108bc57fe5b6000918252602090912001546001600160a01b0316146109395760008054600190920191849081106108ea57fe5b90600052602060002090600a0201600601805490508110610934575050604080518082019091526011815270139bc81d9bdd19481cdd589b5a5d1d1959607a1b6020820152610842565b610889565b61094161181a565b60005b6000858154811061095157fe5b90600052602060002090600a0201600401805490508111610a40576000858154811061097957fe5b90600052602060002090600a0201600401818154811061099557fe5b600091825260209182902060408051606081018252600390930290910180546001600160a01b031683526001810154838501526002810180548351818702810187018552818152949592949386019392830182828015610a1457602002820191906000526020600020905b815481526020019060010190808311610a00575b5050509190925250508151919350506001600160a01b0316331415610a3857610a40565b600101610944565b506000808581548110610a4f57fe5b600091825260209091206007600a90920201015460ff166001811115610a7157fe5b1415610b1c5760008481548110610a8457fe5b90600052602060002090600a0201600201816020015181548110610aa457fe5b906000526020600020906004020160000160008581548110610ac257fe5b90600052602060002090600a0201600201826020015181548110610ae257fe5b9060005260206000209060040201600101604051602001610b049291906123cc565b60405160208183030381529060405292505050610842565b6040518060200160405280600081525092505050610842565b600054606090819067ffffffffffffffff81118015610b5357600080fd5b50604051908082528060200260200182016040528015610b8757816020015b6060815260200190600190039081610b725790505b50905060005b600054811015610c6d5760008181548110610ba457fe5b90600052602060002090600a02016001018054600181600116156101000203166002900480601f016020809104026020016040519081016040528092919081815260200182805460018160011615610100020316600290048015610c495780601f10610c1e57610100808354040283529160200191610c49565b820191906000526020600020905b815481529060010190602001808311610c2c57829003601f168201915b5050505050828281518110610c5a57fe5b6020908102919091010152600101610b8d565b50905090565b6000546000190190565b6000610c8883611592565b610c94575060006105b4565b60005b336001600160a01b031660008581548110610cae57fe5b90600052602060002090600a02016005018281548110610cca57fe5b6000918252602090912001546001600160a01b031614610d22576000805460019092019185908110610cf857fe5b90600052602060002090600a0201600501805490508110610d1d5760009150506105b4565b610c97565b60005b60008581548110610d3257fe5b90600052602060002090600a020160060180549050811015610daf57336001600160a01b031660008681548110610d6557fe5b90600052602060002090600a02016006018281548110610d8157fe5b6000918252602090912001546001600160a01b03161415610da7576000925050506105b4565b600101610d25565b50610db9846115e1565b610dde5760405162461bcd60e51b8152600401610dd590612395565b60405180910390fd5b610de78461160b565b15610e045760405162461bcd60e51b8152600401610dd590612367565b60008481548110610e1157fe5b60009182526020808320600a9290920290910160040180546001808201835591845292829020865160039094020180546001600160a01b0319166001600160a01b03909416939093178355858201519083015560408501518051869392610e7f926002850192910190611844565b5060009150610e8b9050565b60008581548110610e9857fe5b600091825260209091206007600a90920201015460ff166001811115610eba57fe5b1415610f045760008481548110610ecd57fe5b90600052602060002090600a0201600301836020015181548110610eed57fe5b600091825260209091200180546001019055610f7a565b60005b836040015151811015610f785783604001518181518110610f2457fe5b602002602001015160011415610f705760008581548110610f4157fe5b90600052602060002090600a02016003018181548110610f5d57fe5b6000918252602090912001805460010190555b600101610f07565b505b60008481548110610f8757fe5b600091825260208083206006600a9093020191909101805460018181018355918452919092200180546001600160a01b0319163317905591505092915050565b600054606090819067ffffffffffffffff81118015610fe557600080fd5b5060405190808252806020026020018201604052801561101f57816020015b61100c61188b565b8152602001906001900390816110045790505b50905060005b600054811015610c6d576000818154811061103c57fe5b90600052602060002090600a02016000015482828151811061105a57fe5b602090810291909101015152600080548290811061107457fe5b90600052602060002090600a02016001018054600181600116156101000203166002900480601f0160208091040260200160405190810160405280929190818152602001828054600181600116156101000203166002900480156111195780601f106110ee57610100808354040283529160200191611119565b820191906000526020600020905b8154815290600101906020018083116110fc57829003601f168201915b505050505082828151811061112a57fe5b6020026020010151602001819052506000818154811061114657fe5b90600052602060002090600a02016008015482828151811061116457fe5b602002602001015160400181815250506000818154811061118157fe5b90600052602060002090600a02016009015482828151811061119f57fe5b60200260200101516060018181525050600081815481106111bc57fe5b90600052602060002090600a020160070160009054906101000a900460ff168282815181106111e757fe5b60200260200101516080019060018111156111fe57fe5b9081600181111561120b57fe5b905250600101611025565b600054600155825161122f9060029060208601906118c4565b506008805485919060ff19166001838181111561124857fe5b0217905550600880546001600160a01b038716610100908102610100600160a81b0319909216919091179091556009839055600a8281556000805460018181018355918052815492027f290decd9548b62a8d60345a988386fc84ba6bc95484008f6362f93160ef3e56381019283556002805492946112fa937f290decd9548b62a8d60345a988386fc84ba6bc95484008f6362f93160ef3e56490930192808716159091026000190116819004611931565b506002828101805461130f92840191906119a6565b50600382810180546113249284019190611a74565b50600482810180546113399284019190611ab3565b506005828101805461134e9284019190611b42565b50600682810180546113639284019190611b42565b50600782810154908201805460ff9092169160ff19166001838181111561138657fe5b02179055506007828101549082018054610100600160a81b031916610100928390046001600160a01b0316909202919091179055600880830154908201556009918201549101555050505050565b6000805b6000548110156105ae5783600082815481106113f057fe5b90600052602060002090600a020160000154141561158a57336001600160a01b03166000828154811061141f57fe5b60009182526020909120600a909102016007015461010090046001600160a01b0316141561059c576000818154811061145457fe5b90600052602060002090600a020160020160006114719190611b82565b6000818154811061147e57fe5b90600052602060002090600a0201600301600061149b9190611ba6565b60005b835181101561059157600082815481106114b457fe5b90600052602060002090600a02016002018482815181106114d157fe5b60209081029190910181015182546001810184556000938452928290208151805192946004029091019261150a928492909101906118c4565b50602082810151805161152392600185019201906118c4565b506040820151805161153f9160028401916020909101906118c4565b50606082015181600301555050600080838154811061155a57fe5b90600052602060002090600a0201600301828154811061157657fe5b60009182526020909120015560010161149e565b6001016113d8565b6000805b6000548110156115d857600081815481106115ad57fe5b90600052602060002090600a0201600001548314156115d0576001915050610842565b600101611596565b50600092915050565b600042600083815481106115f157fe5b90600052602060002090600a020160080154109050919050565b6000426000838154811061161b57fe5b90600052602060002090600a020160090154109050919050565b8181808214156116465750506117af565b60008660028686030586018151811061165b57fe5b602002602001015190505b818313611783575b8087848151811061167b57fe5b602002602001015110156116945760019092019161166e565b8682815181106116a057fe5b60200260200101518110156116bb5760001990910190611694565b81831361177e578682815181106116ce57fe5b60200260200101518784815181106116e257fe5b60200260200101518885815181106116f657fe5b6020026020010189858151811061170957fe5b602002602001018281525082815250505085828151811061172657fe5b602002602001015186848151811061173a57fe5b602002602001015187858151811061174e57fe5b6020026020010188858151811061176157fe5b602090810291909101019190915252600190920191600019909101905b611666565b818512156117975761179787878785611635565b838312156117ab576117ab87878587611635565b5050505b50505050565b82805482825590600052602060002090810192821561180a579160200282015b8281111561180a57825182546001600160a01b0319166001600160a01b039091161782556020909201916001909101906117d5565b50611816929150611bc4565b5090565b604051806060016040528060006001600160a01b0316815260200160008152602001606081525090565b82805482825590600052602060002090810192821561187f579160200282015b8281111561187f578251825591602001919060010190611864565b50611816929150611be3565b6040518060a0016040528060008152602001606081526020016000815260200160008152602001600060018111156118bf57fe5b905290565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f1061190557805160ff191683800117855561187f565b8280016001018555821561187f579182018281111561187f578251825591602001919060010190611864565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f1061196a578054855561187f565b8280016001018555821561187f57600052602060002091601f016020900482015b8281111561187f57825482559160010191906001019061198b565b828054828255906000526020600020906004028101928215611a685760005260206000209160040282015b82811115611a68578254839083906119ff908290849060026000196101006001841615020190911604611931565b5060018201816001019080546001816001161561010002031660029004611a27929190611931565b5060028281018054611a4c928481019291600019610100600183161502011604611931565b50600382015481600301555050916004019190600401906119d1565b50611816929150611bf8565b82805482825590600052602060002090810192821561187f5760005260206000209182018281111561187f57825482559160010191906001019061198b565b828054828255906000526020600020906003028101928215611b365760005260206000209160030282015b82811115611b3657825482546001600160a01b0319166001600160a01b03909116178255600180840154908301556002808401805485928592611b249291840191611a74565b50505091600301919060030190611ade565b50611816929150611c38565b82805482825590600052602060002090810192821561180a5760005260206000209182015b8281111561180a578254825591600101919060010190611b67565b5080546000825560040290600052602060002090810190611ba39190611bf8565b50565b5080546000825590600052602060002090810190611ba39190611be3565b5b808211156118165780546001600160a01b0319168155600101611bc5565b5b808211156118165760008155600101611be4565b80821115611816576000611c0c8282611c6d565b611c1a600183016000611c6d565b611c28600283016000611c6d565b5060006003820155600401611bf8565b808211156118165780546001600160a01b0319168155600060018201819055611c646002830182611ba6565b50600301611c38565b50805460018160011615610100020316600290046000825580601f10611c935750611ba3565b601f016020900490600052602060002090810190611ba39190611be3565b80356001600160a01b038116811461084257600080fd5b600082601f830112611cd8578081fd5b813567ffffffffffffffff811115611cec57fe5b611cff601f8201601f1916602001612444565b9150808252836020828501011115611d1657600080fd5b8060208401602084013760009082016020015292915050565b600080600080600060a08688031215611d46578081fd5b611d4f86611cb1565b9450602086013560028110611d62578182fd5b9350604086013567ffffffffffffffff811115611d7d578182fd5b611d8988828901611cc8565b9598949750949560608101359550608001359392505050565b600060208284031215611db3578081fd5b5035919050565b60008060408385031215611dcc578182fd5b8235915060208084013567ffffffffffffffff811115611dea578283fd5b8401601f81018613611dfa578283fd5b8035611e0d611e0882612468565b612444565b81815283810190838501858402850186018a1015611e29578687fd5b8694505b83851015611e5257611e3e81611cb1565b835260019490940193918501918501611e2d565b5080955050505050509250929050565b60008060408385031215611e74578182fd5b8235915060208084013567ffffffffffffffff80821115611e93578384fd5b818601915086601f830112611ea6578384fd5b8135611eb4611e0882612468565b81815284810190848601875b84811015611f7157813587016080818e03601f19011215611edf57898afd5b611ee96080612444565b8982013588811115611ef9578b8cfd5b611f078f8c83860101611cc8565b825250604082013588811115611f1b578b8cfd5b611f298f8c83860101611cc8565b8b83015250606082013588811115611f3f578b8cfd5b611f4d8f8c83860101611cc8565b60408301525060809190910135606082015284529287019290870190600101611ec0565b50979a909950975050505050505050565b60008060408385031215611f94578182fd5b8235915060208084013567ffffffffffffffff80821115611fb3578384fd5b9085019060608288031215611fc6578384fd5b604051606081018181108382111715611fdb57fe5b604052611fe783611cb1565b81528383013584820152604083013582811115612002578586fd5b80840193505087601f840112612016578485fd5b82359150612026611e0883612468565b82815284810190848601868502860187018b1015612042578788fd5b8795505b84861015612064578035835260019590950194918601918601612046565b50604083015250949794965093945050505050565b60008282518085526020808601955080818302840101818601855b8481101561210e57601f198684030189528151608081518186526120ba8287018261211b565b91505085820151858203878701526120d2828261211b565b915050604080830151868303828801526120ec838261211b565b6060948501519790940196909652505098840198925090830190600101612094565b5090979650505050505050565b60008151808452815b8181101561214057602081850181015186830182015201612124565b818111156121515782602083870101525b50601f01601f19169290920160200192915050565b6000815460018082166000811461218457600181146121a2576121e0565b60028304607f16865260ff19831660208701526040860193506121e0565b600283048087526121b286612486565b60005b828110156121d65781546020828b01015284820191506020810190506121b5565b8801602001955050505b50505092915050565b6000602080830181845280855180835260408601915060408482028701019250838701855b8281101561223c57603f1988860301845261222a85835161211b565b9450928501929085019060010161220e565b5092979650505050505050565b60006020825261225c6020830184612079565b9392505050565b6000604082526122766040830185612079565b828103602084810191909152845180835285820192820190845b8181101561210e57845183529383019391830191600101612290565b60208082528251828201819052600091906040908185019080840286018301878501865b8381101561233b57603f19898403018552815160a08151855288820151818a8701526122fe8287018261211b565b9150508782015188860152606080830151818701525060808083015192506002831061232657fe5b949094015293860193908601906001016122d0565b509098975050505050505050565b901515815260200190565b60006020825261225c602083018461211b565b602080825260149082015273159bdd1a5b99c8185b1c9958591e48195b99195960621b604082015260600190565b6020808252601a908201527f566f74696e6720686173206e6f74207374617274656420796574000000000000604082015260600190565b600060808252600e60808301526d02cb7ba903b37ba32b2103337b9160951b60a083015260c0602083015261240460c0830185612166565b82810380604085015260018252600160fd1b6020830152604081016060850152506124326040820185612166565b95945050505050565b90815260200190565b60405181810167ffffffffffffffff8111828210171561246057fe5b604052919050565b600067ffffffffffffffff82111561247c57fe5b5060209081020190565b6000908152602090209056fea26469706673582212200283882497bcf296c7c60940abafd2155a2a0488a750ba1dd248be5c9aa85ec264736f6c63430007020033";
        public Bvs_backendDeploymentBase() : base(BYTECODE) { }
        public Bvs_backendDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class CountVotesFunction : CountVotesFunctionBase { }

    [Function("countVotes", typeof(CountVotesOutputDTO))]
    public class CountVotesFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "electionId", 1)]
        public virtual BigInteger ElectionId { get; set; }
    }

    public partial class CreateElectionFunction : CreateElectionFunctionBase { }

    [Function("createElection")]
    public class CreateElectionFunctionBase : FunctionMessage
    {
        [Parameter("address", "electionAdminAddress", 1)]
        public virtual string ElectionAdminAddress { get; set; }
        [Parameter("uint8", "electionVotingSystem", 2)]
        public virtual byte ElectionVotingSystem { get; set; }
        [Parameter("string", "electionName", 3)]
        public virtual string ElectionName { get; set; }
        [Parameter("uint256", "electionStartTimestamp", 4)]
        public virtual BigInteger ElectionStartTimestamp { get; set; }
        [Parameter("uint256", "electionEndTimestamp", 5)]
        public virtual BigInteger ElectionEndTimestamp { get; set; }
    }

    public partial class GetElectionInformationFunction : GetElectionInformationFunctionBase { }

    [Function("getElectionInformation", typeof(GetElectionInformationOutputDTO))]
    public class GetElectionInformationFunctionBase : FunctionMessage
    {

    }

    public partial class GetElectionStringsFunction : GetElectionStringsFunctionBase { }

    [Function("getElectionStrings", "string[]")]
    public class GetElectionStringsFunctionBase : FunctionMessage
    {

    }

    public partial class GetElectoralListFunction : GetElectoralListFunctionBase { }

    [Function("getElectoralList", typeof(GetElectoralListOutputDTO))]
    public class GetElectoralListFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "electionId", 1)]
        public virtual BigInteger ElectionId { get; set; }
    }

    public partial class GetLastElectionIdFunction : GetLastElectionIdFunctionBase { }

    [Function("getLastElectionId", "uint256")]
    public class GetLastElectionIdFunctionBase : FunctionMessage
    {

    }

    public partial class GetVoteFunction : GetVoteFunctionBase { }

    [Function("getVote", "string")]
    public class GetVoteFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "electionId", 1)]
        public virtual BigInteger ElectionId { get; set; }
    }

    public partial class ReplaceElectoralListFunction : ReplaceElectoralListFunctionBase { }

    [Function("replaceElectoralList", "bool")]
    public class ReplaceElectoralListFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "electionId", 1)]
        public virtual BigInteger ElectionId { get; set; }
        [Parameter("tuple[]", "newElectoralList", 2)]
        public virtual List<Candidate> NewElectoralList { get; set; }
    }

    public partial class ReplaceListOfEligibleVotersFunction : ReplaceListOfEligibleVotersFunctionBase { }

    [Function("replaceListOfEligibleVoters", "bool")]
    public class ReplaceListOfEligibleVotersFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "electionId", 1)]
        public virtual BigInteger ElectionId { get; set; }
        [Parameter("address[]", "newEligibleVoterList", 2)]
        public virtual List<string> NewEligibleVoterList { get; set; }
    }

    public partial class VoteFunction : VoteFunctionBase { }

    [Function("vote", "bool")]
    public class VoteFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "electionId", 1)]
        public virtual BigInteger ElectionId { get; set; }
        [Parameter("tuple", "ballot", 2)]
        public virtual Ballot Ballot { get; set; }
    }

    public partial class CountVotesOutputDTO : CountVotesOutputDTOBase { }

    [FunctionOutput]
    public class CountVotesOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("tuple[]", "candidateRanking", 1)]
        public virtual List<Candidate> CandidateRanking { get; set; }
        [Parameter("uint256[]", "voteCount", 2)]
        public virtual List<BigInteger> VoteCount { get; set; }
    }



    public partial class GetElectionInformationOutputDTO : GetElectionInformationOutputDTOBase { }

    [FunctionOutput]
    public class GetElectionInformationOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("tuple[]", "", 1)]
        public virtual List<TmpElectionObject> ReturnValue1 { get; set; }
    }

    public partial class GetElectionStringsOutputDTO : GetElectionStringsOutputDTOBase { }

    [FunctionOutput]
    public class GetElectionStringsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string[]", "", 1)]
        public virtual List<string> ReturnValue1 { get; set; }
    }

    public partial class GetElectoralListOutputDTO : GetElectoralListOutputDTOBase { }

    [FunctionOutput]
    public class GetElectoralListOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("tuple[]", "candidates", 1)]
        public virtual List<Candidate> Candidates { get; set; }
    }

    public partial class GetLastElectionIdOutputDTO : GetLastElectionIdOutputDTOBase { }

    [FunctionOutput]
    public class GetLastElectionIdOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class GetVoteOutputDTO : GetVoteOutputDTOBase { }

    [FunctionOutput]
    public class GetVoteOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }






}
