using Microsoft.AspNetCore.Mvc;
using WebUygulamaProjesi1.Utility;
using WebUygulamaProjesi1.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;

namespace WebUygulamaProjesi1.Controllers
{
	public class KiralamaController : Controller
	{
		private readonly IKiralamaRepository _kiralamaRepository;
		private readonly IKitapRepository _kitapRepository;
		public readonly IWebHostEnvironment _webHostEnvironment;
		public KiralamaController(IKiralamaRepository kiralamaRepository, IKitapRepository kitapRepository, IWebHostEnvironment webHostEnvironment)
		{
			_kiralamaRepository = kiralamaRepository;
			_kitapRepository = kitapRepository;
			_webHostEnvironment = webHostEnvironment;


		}
		public IActionResult Index()
		{
			
			List<Kiralama> objKiralamaList = _kiralamaRepository.GetAll(includeProps: "Kitap").ToList();
			return View(objKiralamaList);
		}

		public IActionResult EkleGuncelle(int? id)
		{
			IEnumerable<SelectListItem> KitapList = _kitapRepository.GetAll()
			.Select(k => new SelectListItem
			{
				Text = k.KitapAdi,
				Value = k.Id.ToString()
			});
			ViewBag.KitapTuruList = KitapList;
			if (id == null || id == 0)
			{
				//ekle
				return View();
			}
			else
			{
				//güncelleme
				Kiralama? kiralamaVt = _kiralamaRepository.Get(u => u.Id == id); //Expression<Func<Ti bool>> filtre
				if (kiralamaVt == null)
				{
					return NotFound();
				}
				return View(kiralamaVt);
			}

		}
		[HttpPost]
		public IActionResult EkleGuncelle(Kiralama kiralama)
		{
			if (ModelState.IsValid)
			{
			
				if (kiralama.Id == 0)
				{
					_kiralamaRepository.Ekle(kiralama);
					TempData["basarili"] = "Yeni Kiralaam Kaydı başarıyla oluşturuldu!";
				}
				else
				{
					_kiralamaRepository.Guncelle(kiralama);
					TempData["basarili"] = "Kiralama Kayıt güncelleme başarılı!";
				}

				//_kiralamaRepository.Ekle(kiralama);
				_kiralamaRepository.Kaydet(); //SaveChanges yapılmazsa veri tabanına eklenmez!
				//TempData["basarili"] = "Yeni Kitap Türü Başarıyla Oluşturuldu!!!";
				return RedirectToAction("Index", "Kiralama");
			}
			return View();
		}

		public IActionResult Sil(int? id)
		{
            IEnumerable<SelectListItem> KitapList = _kitapRepository.GetAll()
            .Select(k => new SelectListItem
            {
                Text = k.KitapAdi,
                Value = k.Id.ToString()
            });
            ViewBag.KitapTuruList = KitapList;
            if (id == null)
			{
				return NotFound();
			}
			Kiralama? kiralamaVt = _kiralamaRepository.Get(u => u.Id == id);
			if (kiralamaVt == null)
			{
				return NotFound();
			}
			return View(kiralamaVt);
		}
		[HttpPost, ActionName("Sil")]
		public IActionResult SilPOST(int? id)
		{
			Kiralama? kiralama = _kiralamaRepository.Get(u => u.Id == id);
			if (kiralama == null)
			{
				return NotFound();
			}
			_kiralamaRepository.Sil(kiralama);
			_kiralamaRepository.Kaydet();
			TempData["basarili"] = "Kayıt Silme İşlemi Başarılı!!!";
			return RedirectToAction("Index", "Kiralama");

		}



	}
}