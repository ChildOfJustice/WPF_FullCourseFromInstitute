using Task_8.Algorythms;

namespace SardorVersion.AsyncCypher
{
    public class TaskProperties
    {
        public int BlockNumber;
        public CypherAlgorithm Algorithm;
        public byte[] Data;

        public TaskProperties(int blockNumber, CypherAlgorithm algorithm, byte[] data)
        {
            BlockNumber = blockNumber;
            Algorithm = algorithm;
            Data = data;
        }
    }
}