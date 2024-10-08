﻿using WebUygulamaProjesi1.Utility;
using System.Linq.Expressions;

namespace WebUygulamaProjesi1.Models
{
	public class KitapTuruRepository : Repository<KitapTuru>, IKitapTuruRepository
	{
		private UygulamaDbContext _uygulamaDbContext;
		public KitapTuruRepository(UygulamaDbContext uygulamaDbContext) : base(uygulamaDbContext)
		{
			_uygulamaDbContext= uygulamaDbContext;
		}

		public void Guncelle(KitapTuru kitapTuru)
		{
			_uygulamaDbContext.Update(kitapTuru);
		}

		public void Kaydet()
		{
			_uygulamaDbContext.SaveChanges();
		}
	}
}
