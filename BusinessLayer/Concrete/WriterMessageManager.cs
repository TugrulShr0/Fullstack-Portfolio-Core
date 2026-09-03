namespace BusinessLayer.Concrete;

using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

public class WriterMessageManager(IWriterMessageDal dal) : IWriterMessageService
{
    public void TAdd(WriterMessage t) => dal.Insert(t);
    public void TDelete(WriterMessage t) => dal.Delete(t);
    public void TUpdate(WriterMessage t) => dal.Update(t);
    public List<WriterMessage> TGetList() => dal.GetList();
    public WriterMessage? TGetByID(int id) => dal.GetByID(id);

    public async Task TAddAsync(WriterMessage t) => await dal.InsertAsync(t);
    public async Task TDeleteAsync(WriterMessage t) => await dal.DeleteAsync(t);
    public async Task TUpdateAsync(WriterMessage t) => await dal.UpdateAsync(t);
    public async Task<List<WriterMessage>> TGetListAsync() => await dal.GetListAsync();
    public async Task<WriterMessage?> TGetByIDAsync(int id) => await dal.GetByIDAsync(id);

    public List<WriterMessage> GetListReceiverMessage(string p) => dal.GetbyFilter(x => x.Receiver == p);
    public List<WriterMessage> GetListSenderMessage(string p) => dal.GetbyFilter(x => x.Sender == p);
    public async Task<List<WriterMessage>> GetListReceiverMessageAsync(string p) => await dal.GetbyFilterAsync(x => x.Receiver == p);
    public async Task<List<WriterMessage>> GetListSenderMessageAsync(string p) => await dal.GetbyFilterAsync(x => x.Sender == p);
}
