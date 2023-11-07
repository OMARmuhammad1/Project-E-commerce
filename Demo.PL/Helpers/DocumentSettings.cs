﻿using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Demo.PL.Helpers
{
	public static class DocumentSettings
	{
		//Upload
		public static string UploadFile(IFormFile file, string FolderName)
		{
			//1. Get Located Folder Path 
			string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);
			
			//2.Get File Name and Make it Unique
			string FileName = $"{Guid.NewGuid()}+{file.FileName}"; 
            //3.Get File Path[Folder Path + FileName]
			string FilePath=Path.Combine(FolderPath, FileName);
            //4.Save File As Streams
			using var FS=new FileStream(FilePath, FileMode.Create);
			file.CopyTo(FS);
			//5.Return File Name
			return FileName;

		}
		 
		//Delete

		public static void DeleteFile(string FileName, string FolderName)
		{

			if (FileName is not null && FolderName is not null)
			{
				string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName, FileName);
				if (File.Exists(FilePath))
				{
					File.Delete(FilePath);
				}
			}

		}
	}
}
