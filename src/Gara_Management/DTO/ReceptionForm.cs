﻿using Gara_Management.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DTO
{
    public class ReceptionForm
    {
        private string iDRec;
        public string IDRec { get => iDRec; set => iDRec = value; }

        private string iDCus;
        public string IDCus { get => iDCus; set => iDCus = value; }

        private string iDBrand;
        public string IDBrand { get => iDBrand; set => iDBrand = value; }

        private string iDGara;
        public string IDGara { get => iDGara; set => iDGara = value; }

        private string numberPlate;
        public string NumberPlate { get => numberPlate; set => numberPlate = value; }

        private DateTime receptionDate;
        public DateTime ReceptionDate { get => receptionDate; set => receptionDate = value; }

        private bool statusRec;
        public bool StatusRec { get => statusRec; set => statusRec = value; }

        public ReceptionForm(string iDRec, string iDCus, string iDBrand, string iDGara, string numberPlate, DateTime receptionDate, bool statusRec)
        {
            this.IDRec = iDRec;
            this.IDCus = iDCus;
            this.IDBrand = iDBrand;
            this.IDGara = iDGara;
            this.NumberPlate = numberPlate;
            this.ReceptionDate = receptionDate;
            this.StatusRec = statusRec;
        }

        public ReceptionForm(DataRow row)
        {
            this.IDRec = row["ID_REC"].ToString().Trim();
            this.IDCus = row["ID_CUS"].ToString().Trim();
            this.IDBrand = row["ID_BRAND"].ToString().Trim();
            this.IDGara = row["ID_GARA"].ToString().Trim();
            this.NumberPlate = row["NUMBER_PLATES"].ToString().Trim();
            this.ReceptionDate = Convert.ToDateTime(row["RECEPTION_DATE"].ToString().Trim());
            this.StatusRec = Convert.ToBoolean(row["STATUS_REC"].ToString().Trim());
        }

        //kiểm tra có giống nhau không
        public bool IsEqual(ReceptionForm other)
        {
            if (this.iDRec != other.iDRec) return false;
            if (this.iDCus != other.iDCus) return false;
            if (this.iDBrand != other.iDBrand) return false;
            if (this.iDGara != other.iDGara) return false;
            if (this.numberPlate != other.numberPlate) return false;
            if (this.receptionDate != other.receptionDate) return false;
            if (this.statusRec != other.statusRec) return false;
            return true;
        }
    }
}
