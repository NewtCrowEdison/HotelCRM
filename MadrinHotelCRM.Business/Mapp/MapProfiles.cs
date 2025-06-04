using AutoMapper;
using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Entities.Models;

namespace MadrinHotelCRM.Business.Mapp
{
    public class MapProfiles : Profile
    {
        public MapProfiles()
        {

            CreateMap<EkPaket, EkPaketDTO>().ReverseMap();
            CreateMap<Etiket, EtiketDTO>().ReverseMap();
            CreateMap<Fatura, FaturaDTO>().ReverseMap();
            CreateMap<GenelTakip, GenelTakipDTO>().ReverseMap();
            CreateMap<GeriBildirim, GeriBildirimDTO>().ReverseMap();
            CreateMap<Musteri, MusteriDTO>().ReverseMap();
            CreateMap<MusteriEtiket, MusteriEtiketDTO>().ReverseMap();
            CreateMap<MusteriEtkilesim, MusteriEtkilesimDTO>().ReverseMap();
            CreateMap<Oda, OdaDTO>().ReverseMap();
            CreateMap<OdaTarife, OdaTarifeDTO>().ReverseMap();
            CreateMap<OdaTipi, OdaTipiDTO>().ReverseMap();
            CreateMap<Odeme, OdemeDTO>().ReverseMap();
            // CreateMap<Personel, PersonelDTO>();
            // .ForMember(dest => dest.DepartmanAdi, opt => opt.MapFrom(src => src.Departman.DepartmanAdi));
            CreateMap<Personel, PersonelDTO>()
            .ForMember(dest => dest.KullaniciId, opt => opt.MapFrom(src => src.KullaniciId))
            .ReverseMap();
            // CreateMap<PersonelDTO, Personel>();
            CreateMap<Rezervasyon, RezervasyonDTO>().ReverseMap();
            CreateMap<RezervasyonPaket, RezervasyonPaketDTO>().ReverseMap();
            CreateMap<SistemLog, SistemLogDTO>().ReverseMap();
            CreateMap<Tarife, TarifeDTO>().ReverseMap();
            CreateMap<Departman, DepartmanDTO>().ReverseMap();
            CreateMap<MusteriRezervasyon, MusteriRezervasyonDTO>().ReverseMap();

        }
    }
}