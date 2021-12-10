using System;
using System.Collections.Generic;
using System.Text;

namespace Clobber__alpha_beta_pruning
{
    public static class Heuristics
    {
        public static long F(char[,] array)
        {
            long playerPoints = 0, computerPoints = 0;
            // Оценивает ценность текущей доски
            /* Идея евристике для твоей игры ну прямочень простая,
             * тебе даже не нужно придумывать ничего (у меня тм был целый велосипед)
             * Просто напросто проверяй каждую шашку игрока №1, которая может побить хотя бы
             * одного противника. За каждую такую шашку давай игроку №1 +1 бал
             * Теже действия дляигрока №2, а вконце просто отними очки ПК от очков Игрока
            */
            return computerPoints - playerPoints;
        }
    }
}
