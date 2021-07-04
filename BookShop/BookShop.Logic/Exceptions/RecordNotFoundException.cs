using System;

namespace BookShop.Logic.Exceptions
{
    public sealed class RecordNotFoundException : Exception
    {
        public RecordNotFoundException(int recordId, Type recordType) : base(
            $"Record of type {recordType.Name} with id {recordId} was not found")
        {
        }
    }
}