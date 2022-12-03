using Application.DTOs;
using Domain;

namespace Application.Interfaces;

public interface IItemService
{
    Item Create(PostItemDTO postItemDto); //Application layer will parse this into an Item
    Item Read(int id);
    List<Item> ReadAll();
    Item Update(int id, PutItemDTO dto); //To be able to check whether the body and header corresponds
    List<Item> UpdateNameRange(List<PatchItemNameDTO> dtos);

    Item UpdateQuantity(int id, Movement movement);
    Item Delete(int id);
}