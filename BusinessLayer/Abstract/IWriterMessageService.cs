namespace BusinessLayer.Abstract;

using EntityLayer.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IWriterMessageService : IGenericService<WriterMessage>
{
    List<WriterMessage> GetListReceiverMessage(string p);
    List<WriterMessage> GetListSenderMessage(string p);
    Task<List<WriterMessage>> GetListReceiverMessageAsync(string p);
    Task<List<WriterMessage>> GetListSenderMessageAsync(string p);
}
