using System.IO;
using DemoParser.Utils;
using Xunit;

namespace DemoParser.Tests.FileHashTests
{
    public class FileHashTests
    {
        private const string InitialInputPath = "FileHashTests\\Input_Initial.cs";
        private const string IdenticalInputPath = "FileHashTests\\Input_Identical.cs";
        private const string ChangedInputPath = "FileHashTests\\Input_Changed.cs";
        private const string NonExistingPath = "FileHashTests\\NotExists.cs";

        [Fact]
        public void CalculatesFileHash()
        {
            var result = Act(InitialInputPath);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void ReturnsEqualHash_WhenBothFilesAreIdentical()
        {
            var initialResult = Act(InitialInputPath);
            var identicalResult = Act(IdenticalInputPath);
            Assert.Equal(initialResult, identicalResult);
        }

        [Fact]
        public void ReturnsDifferentHash_WhenFilesAreDifferent()
        {
            var initialResult = Act(InitialInputPath);
            var changedResult = Act(ChangedInputPath);
            Assert.NotEqual(initialResult, changedResult);
        }

        [Fact]
        public void ThrowsException_WhenFileDoesNotExist()
        {
            try
            {
                Act(NonExistingPath);
                Assert.True(false, "Should have thrown an exception.");
            }
            catch (FileNotFoundException)
            {
            }
        }

        private string Act(string filePath)
        {
            var calculator = new FileHashCalculator();
            var fileHash = calculator.Get(filePath);
            return fileHash;
        }
    }
}
