namespace BusinessLayer.Concrete;

using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

public class MessageManager(IMessageDal dal) : IMessageService
{
    public void TAdd(Message t) => dal.Insert(t);
    public void TDelete(Message t) => dal.Delete(t);
    public void TUpdate(Message t) => dal.Update(t);
    public List<Message> TGetList() => dal.GetList();
    public Message? TGetByID(int id) => dal.GetByID(id);

    public async Task TAddAsync(Message t) => await dal.InsertAsync(t);
    public async Task TDeleteAsync(Message t) => await dal.DeleteAsync(t);
    public async Task TUpdateAsync(Message t) => await dal.UpdateAsync(t);
    public async Task<List<Message>> TGetListAsync() => await dal.GetListAsync();
    public async Task<Message?> TGetByIDAsync(int id) => await dal.GetByIDAsync(id);
}
