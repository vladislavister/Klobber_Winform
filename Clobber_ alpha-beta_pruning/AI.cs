using System;
using System.Collections.Generic;
using System.Text;

namespace Clobber__alpha_beta_pruning
{
    public static class AI
    {
        public static (char[,], long) MiniMax(char[,] array, int depth, long alpha, long beta, bool miximizigPlayerTurnToMove)
        {
            if (depth == 0 || CheckWinner(array, 'C'))
                return (array, Heuristics.F(array));

            if (miximizigPlayerTurnToMove)
            {
                (char[,], long) maxEval = (null, -999999999999999999);
                List<char[,]> children = GetAllChildren(array, 'C');
                foreach (var child in children)
                {
                    (char[,], long) eval = MiniMax(child, depth - 1, alpha, beta, false);
                    char[,] buffArray = new char[5, 5];
                    Array.Copy(child, 0, buffArray, 0, array.Length);
                    maxEval = (maxEval.Item2 < eval.Item2) ? (buffArray, eval.Item2) : maxEval;

                    alpha = (alpha < eval.Item2) ? eval.Item2 : alpha;
                    if (beta <= alpha)
                        break;
                }
                return maxEval;
            }
            else
            {
                (char[,], long) minEval = (null, 999999999999999999);
                List<char[,]> children = GetAllChildren(array, 'P');
                foreach (var child in children)
                {
                    (char[,], long) eval = MiniMax(child, depth - 1, alpha, beta, true);
                    char[,] buffArray = new char[5, 5];
                    Array.Copy(child, 0, buffArray, 0, array.Length);
                    minEval = (minEval.Item2 > eval.Item2) ? (buffArray, eval.Item2) : minEval;

                    beta = (beta > eval.Item2) ? eval.Item2 : beta;
                    if (beta <= alpha)
                        break;
                }
                return minEval;
            }
        }

        public static bool CheckWinner(char[,] array, char symbol)
        {
            // Проверяет не победил ли один из игроков (symbol решает чью победу проверяем)
            // Просто проверяем есть ли у противника хоть 1 фишка чтобы походить
            // Если есть, то return false если же нету, то return true
            // ВНИМАНИЕ! параметр symbol отвечает за то, чью ПОБЕДУ мы проверяем
            // поетому, если symbol = 'P', то проверяем есть ли у ПК хоть один ход (победил ли игрок)
            // соответственно , если symbol = 'C', то проверяем есть ли у игрока хотя бы один ход (победил ли ПК)
            // крч меняем symbol на противоположный и проверяем
            return true;
        }

        public static List<char[,]> GetAllChildren(char[,] array, char symbol)
        {
            List<char[,]> children = new List<char[,]>();

            // Возвращает всех детей твоей доски для одного из игроков (symbol решает чьи шашки двигаются)
            // Пытаешся сдвинуть каждую доступную шашку вверх, вниз, влево и вправо 
            // ВНИМАНИЕ! каждое движение делается на отцовской доске (на одном ребёнке может быть
            // не более одной перемены/сдвига)
            // Если получилось сдвинуть шашку, то добавляешь эту доску в список детей,
            // Если не получилось, то и хуй с ним
            // Кстати, перед тем, как передвигать фигуру не просто сделай childArray = fatherArray
            // А именно скопируй fatherArray в childArray спомощью
            // Array.Copy(fatherArray, 0, childArray, 0, array.Length);
            // ибо масивы ссылочний тип данных и если так не сделать, то будетпизда

            return children;
        }
    }
}
