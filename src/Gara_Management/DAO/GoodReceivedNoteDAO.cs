using Gara_Management.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DAO
{
    public class GoodReceivedNoteDAO
    {
        private GoodReceivedNoteDAO() { }
        private static GoodReceivedNoteDAO instance;

        public static GoodReceivedNoteDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new GoodReceivedNoteDAO();
                return instance;
            }
            private set { instance = value; }
        }

        public List<GoodReceivedNote> LoadGoodReceivedNoteList()
        {
            List<GoodReceivedNote> goodReceivedNoteList = new List<GoodReceivedNote>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM GOOD_RECEIVED_NOTES");
            foreach (DataRow item in data.Rows)
            {
                GoodReceivedNote goodReceivedNote = new GoodReceivedNote(item);
                goodReceivedNoteList.Add(goodReceivedNote);
            }
            return goodReceivedNoteList;
        }

    }
}
