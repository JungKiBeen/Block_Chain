using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Block_Chain_Impl_Ver01
{
    [Serializable]
    public class Block
    {
        public Block next_block { get; set; }                  // 원래 비트코인에는 블록의 브랜치가 존재하나, 여기서는 고려하지 않고 구현함; 블록체인에 블록 추가가 확정될 때 추가함(2)
        public Header header { get; set; }                      // 블록 생성시 계산함(0)
        public Merkle merkle_root { get; set; }

        public int block_number { get; set; }                   // block_chain.Count + temporary_transactions.Count; 
        public int height { get; set; }                         // hblock_chain.Count + temporary_transactions.Count + 1; 
        public int tx_counter { get; set; }

        public string from_miner { get; set; }                  // from_miner만 블록체인에 추가할 때 계산 
        public string mining_time_stamp { get; set; }

        public List<Transaction> transactions { get; set; }      // 원래는 transaction의 해쉬값을 저장하나, 여기서는 트랜잭션 데이터를 저장하도록 구현함
        public List<Merkle> tx_merkles { get; set; }               // 트랜잭션의 Merkle를 가지고 있음

        public Block(Header header, int block_number, int height)
        {
            next_block = null;
            this.header = header;

            transactions = new List<Transaction>();
            tx_merkles = new List<Merkle>();

            this.block_number = block_number;
            this.height = height;
            from_miner = "";
            tx_counter = 0;
            mining_time_stamp = "";

        }

        public static Block create_genesis(int tx_unit)
        {
            Header gene_header = new Header("", "", null, "", "");
            gene_header.nonce = "";
            Block genesis = new Block(gene_header,0 ,1);

            for (int i = 0; i < tx_unit; i++)
            {
                Transaction tx = new Transaction("", "", "");
                genesis.transactions.Add(tx);
                string hash = Algorithms.SHA256Double(tx.get_transaction());
                Merkle new_merkle = new Merkle(null, null, hash, genesis.tx_merkles.Count);

                genesis.tx_merkles.Add(new_merkle);
                genesis.tx_counter++;
            }

            genesis.merkle_root = Algorithms.build_merkle(genesis.tx_merkles);
            genesis.header.merkle_root_hash = genesis.merkle_root.hash;

            return genesis;
        }
  
    }
}
