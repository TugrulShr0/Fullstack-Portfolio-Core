namespace BusinessLayer.Concrete;

using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ToDoListManager(IToDoListDal dal) : IToDoListService
{
    public void TAdd(ToDoList t) => dal.Insert(t);
    public void TDelete(ToDoList t) => dal.Delete(t);
    public void TUpdate(ToDoList t) => dal.Update(t);
    public List<ToDoList> TGetList() => dal.GetList();
    public ToDoList? TGetByID(int id) => dal.GetByID(id);

    public async Task TAddAsync(ToDoList t) => await dal.InsertAsync(t);
    public async Task TDeleteAsync(ToDoList t) => await dal.DeleteAsync(t);
    public async Task TUpdateAsync(ToDoList t) => await dal.UpdateAsync(t);
    public async Task<List<ToDoList>> TGetListAsync() => await dal.GetListAsync();
    public async Task<ToDoList?> TGetByIDAsync(int id) => await dal.GetByIDAsync(id);
}
