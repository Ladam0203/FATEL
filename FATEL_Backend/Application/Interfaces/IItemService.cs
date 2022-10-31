using Application.DTOs;
using Domain;

namespace Application.Interfaces;

public interface IItemService
{
    public Item Create(PostItemDTO postClientDto); //Application layer will parse this into an Item
    public Item Read(int id);
    public List<Item> ReadAll();
    public Item Update(int id, Item item); //To be able to check whether the body and header corresponds
    public Item Delete(int id);
}