using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;




namespace Noah
{

    public interface ILoader<Key, Item>
		{
			Dictionary<Key, Item> MakeDic();
			bool Validate();
		}

		public class DataManager
		{

			public Dictionary<int, Noah.Record> AddressSpaceDatas { get; private set; }


			public void Init()
			{
		
				// AddressSpaceDatas
				AddressSpaceDatas = LoadJson<AddressSpaceData, int, Record>("AddressSpaceData").MakeDic();


		}

			private Item LoadSingleXml<Item>(string name)
			{
				XmlSerializer xs = new XmlSerializer(typeof(Item));
				TextAsset textAsset = Resources.Load<TextAsset>("Data/" + name);
				using (MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(textAsset.text)))
					return (Item)xs.Deserialize(stream);
			}

			private Loader LoadXml<Loader, Key, Item>(string name) where Loader : ILoader<Key, Item>, new()
			{
				XmlSerializer xs = new XmlSerializer(typeof(Loader));
				TextAsset textAsset = Resources.Load<TextAsset>("Data/" + name);
				using (MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(textAsset.text)))
					return (Loader)xs.Deserialize(stream);
			}


			private Loader LoadJson<Loader, Key, Item>(string path) where Loader : ILoader<Key, Item>
			{
				TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
				return JsonUtility.FromJson<Loader>(textAsset.text);
			}

		}


}
