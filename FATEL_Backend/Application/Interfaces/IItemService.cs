using Application.DTOs;
using Domain;

namespace Application.Interfaces;

public interface IItemService
{
    public Item Create(PostItemDTO postItemDto); //Application layer will parse this into an Item
    public Item Read(int id);
    public List<Item> ReadAll();
    public Item Update(int id, PutItemDTO dto); //To be able to check whether the body and header corresponds
    public Item UpdateQuantity(int id, Movement movement);
    public Item Delete(int id);
}