using LMS_Elibraty.Data;
using Microsoft.EntityFrameworkCore;

namespace LMS_Elibraty.Services.Classes
{
    public class ClassService:IClassService
    {
        private readonly LMSElibraryContext _context;

        public ClassService(LMSElibraryContext context)
        {
            _context = context;
        }
        private int countSH = 0;
        private int countMT = 0;
        private int countTP = 0;
        private int countQT = 0;
        private int countKS = 0;
        private int countDL = 0;
        private int countLH = 0;
        private int countNH = 0;
        private int countKT = 0;
        private int countTC = 0;
        private int countTH = 0;
        private int countXD = 0;
        private int countXC = 0;
        private int countNT = 0;
        private int countTT = 0;
        private int countTA = 0;
        private int countTN = 0;
        private int countDC = 0;
        private int countCT = 0;
        private int countCK = 0;
        private int countTD = 0;
        private int countLK = 0;
        private int countKI = 0;
        private int countTL = 0;
        private int countMA = 0;
        private int countTY = 0;
        public async Task<Class> Create(Class cls)
        {
            cls.Id = GenerateClassId(cls.FacultyId);
            _context.Classes.Add(cls);
            await _context.SaveChangesAsync();
            return cls;
        }

        public async Task<Class?> Details(string id)
        {
            return await _context.Classes.FindAsync(id);
        }

        public async Task<IEnumerable<Class>> All()
        {
            return await _context.Classes.ToListAsync();
        }

        public async Task<Class?> Update(string id, Class cls)
        {
            var existingClass = await _context.Classes.FindAsync(id);
            if (existingClass == null)
                return null;
            existingClass.Name = cls.Name;
            await _context.SaveChangesAsync();
            return existingClass;
        }

        public async Task<bool> Delete(string id)
        {
            var cls = await _context.Classes.FindAsync(id);
            if (cls == null)
                return false;
            _context.Classes.Remove(cls);
            await _context.SaveChangesAsync();
            return true;
        }
        private string GenerateClassId(int id)
        {
            string prefix;
            int counter;
            if (id == 1)
            {
                prefix = "SH";
                counter = ++countSH;
            }
            else if (id == 2)
            {
                prefix = "MT";
                counter = ++countMT;
            }
            else if (id == 3)
            {
                prefix = "TP";
                counter = ++countTP;
            }
            else if (id == 4)
            {
                prefix = "QT";
                counter = ++countQT;
            }
            else if (id == 5)
            {
                prefix = "KS";
                counter = ++countKS;
            }
            else if (id == 6)
            {
                prefix = "DL";
                counter = ++countDL;
            }
            else if (id == 7)
            {
                prefix = "LH";
                counter = ++countLH;
            }
            else if (id == 8)
            {
                prefix = "NH";
                counter = ++countNH;
            }
            else if (id == 9)
            {
                prefix = "KT";
                counter = ++countKT;
            }
            else if (id == 10)
            {
                prefix = "TC";
                counter = ++countTC;
            }
            else if (id == 11)
            {
                prefix = "TH";
                counter = ++countTH;
            }
            else if (id == 12)
            {
                prefix = "XD";
                counter = ++countXD;
            }
            else if (id == 13)
            {
                prefix = "XC";
                counter = ++countXC;
            }
            else if (id == 14)
            {
                prefix = "NT";
                counter = ++countNT;
            }
            else if (id == 15)
            {
                prefix = "TT";
                counter = ++countTT;
            }
            else if (id == 16)
            {
                prefix = "TA";
                counter = ++countTA;
            }
            else if (id == 17)
            {
                prefix = "TN";
                counter = ++countTN;
            }
            else if (id == 18)
            {
                prefix = "DC";
                counter = ++countDC;
            }
            else if (id == 19)
            {
                prefix = "CT";
                counter = ++countCT;
            }
            else if (id == 20)
            {
                prefix = "CK";
                counter = ++countCK;
            }
            else if (id == 21)
            {
                prefix = "TD";
                counter = ++countTD;
            }

            else if (id == 22)
            {
                prefix = "LK";
                counter = ++countLK;
            }
            else if (id == 23)
            {
                prefix = "KI";
                counter = ++countKI;
            }
            else if (id == 24)
            {
                prefix = "TL";
                counter = ++countTL;
            }
            else if (id == 25)
            {
                prefix = "MA";
                counter = ++countMA;
            }
            else
            {
                prefix = "TY";
                counter = ++countTY;
            }
            string yearDigits = DateTime.Now.Year.ToString().Substring(2);
            return $"DH{yearDigits}{prefix}{counter:D3}";
        }
    }
}
