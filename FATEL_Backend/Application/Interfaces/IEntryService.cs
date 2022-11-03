using Application.DTOs;
using Domain;

namespace Application.Interfaces;

public interface IEntryService
{
    Entry Create(PostEntryDTO dto);
    List<Entry> ReadAll();
}