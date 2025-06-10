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

            // Oda <-> OdaDTO mapping
            CreateMap<Oda, OdaDTO>()
                // Sadece OdaTipiAdı'yı map et; nav-prop'ü göstermek için kullanırız
                .ForMember(dest => dest.OdaTipiAdi, opt => opt.MapFrom(src => src.OdaTipi.OdaTurAd))
                // Client'tan gelmediği için nav-prop'ü ignore ederiz
                .ForMember(dest => dest.OdaTipi, opt => opt.Ignore());

            CreateMap<OdaDTO, Oda>()
                // FK olarak OdaTipiId'yi kullan
                .ForMember(dest => dest.OdaTipiId, opt => opt.MapFrom(src => src.OdaTipiId))
                // Nav-prop olarak ekleme/güncelleme yapma!! (yeni satır olarak eklememe)
                .ForMember(dest => dest.OdaTipi, opt => opt.Ignore())
                .ForMember(dest => dest.OdaNumarasi, opt => opt.MapFrom(src => src.OdaNumarasi))
                .ForMember(dest => dest.Durum, opt => opt.MapFrom(src => src.Durum));

            CreateMap<OdaTarife, OdaTarifeDTO>().ReverseMap();
            CreateMap<OdaTipi, OdaTipiDTO>().ReverseMap();
            CreateMap<Odeme, OdemeDTO>().ReverseMap();

            CreateMap<Personel, PersonelDTO>()
                .ForMember(dest => dest.KullaniciId, opt => opt.MapFrom(src => src.KullaniciId))
                .ReverseMap();

            CreateMap<Rezervasyon, RezervasyonDTO>().ReverseMap();
            CreateMap<RezervasyonPaket, RezervasyonPaketDTO>().ReverseMap();
            CreateMap<SistemLog, SistemLogDTO>().ReverseMap();
            CreateMap<Tarife, TarifeDTO>().ReverseMap();
            CreateMap<Departman, DepartmanDTO>().ReverseMap();
            CreateMap<MusteriRezervasyon, MusteriRezervasyonDTO>().ReverseMap();
        }
    }
}
