using System.Collections.Generic;
using System.Linq;
using Tradesoft.Analogues.Database.Models;
using Tradesoft.Analogues.Database.Repos;
using Path = System.Collections.Generic.List<Tradesoft.Analogues.Database.Models.Product>;

namespace Tradesoft.Analogues.Core
{
    public class AnalogyGraph
    {
        private readonly AnalogyRepository analogyRepository;

        public AnalogyGraph()
        {
            analogyRepository = new AnalogyRepository();
        }

        private List<Path> paths;
        public List<Path> GetPath(Product p1, Product p2, int depth)
        {
            paths = new List<Path>();
            GetPath(p1.Id, p2.Id, depth, new Path() { p1 }, new Dictionary<int, bool>());
            return paths;
        }

        private void GetPath(int id1, int id2, int depth, Path curPath, Dictionary<int, bool> isVisited, bool contunueSearch = true)
        {
            if (id1 == id2)
            {
                paths.Add(new Path(curPath));
                return;
            }

            isVisited[id1] = true;
            int curPathId = curPath.Last().Id;
            bool IsVisited(Analogy a)
            {
                int id = GetProduct(a).Id;
                return isVisited.ContainsKey(id) && isVisited[id];
            }
            Product GetProduct(Analogy a)
            {
                return a.Product1.Id == curPathId ? a.Product2 : a.Product1;
            }

            var analogues = contunueSearch ? analogyRepository.GetAnalogues(curPathId).ToList() : new List<Analogy>();
            foreach (var item in analogues)
            {
                if (!IsVisited(item) && curPath.Count < depth + 1)
                {
                    var p = GetProduct(item);
                    curPath.Add(p);
                    GetPath(p.Id, id2, depth, curPath, isVisited, item.TrustLevel > 0);
                    curPath.Remove(p);
                }
            }
            isVisited[id1] = false;
        }
    }
}
