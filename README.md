Bienvenue dans Rat Buster !

Ce programme est une version revisitée du snake avec quelques différences. Ce document liste les plus marquantes, et les approches de développement choisies.

● Faire usage du format PC et exploiter une grille plus vaste ! Le serpent a également la possibilité de réapparaître de l’autre côté de la grille s’il touche les bords.
        ■ On a étendu la classe snake avec des méthodes pour checker si la tête du serpent est au bord de la grille, et renforcé la méthode Move() pour gérer ces cas.
  
● Pour le plus grand plaisir des amateurs de reptiles, notre serpent ne mange plus de pommes, mais bien des rats dont il recherche les cachettes sur la grille.
        ■ Toutes les classes/variables/méthodes reflètent ce changement dans le nommage.
    
● La position des cachettes est invisible pour le joueur !
    ○ En cas de panique, on peut dépenser une charge de super sens (Middle Mouse Button) pour afficher la prochaine cachette.
    ○ En cas de besoin (débogage…) on peut afficher les cachettes avec Enter.
  
● La vitesse de notre serpent est également contrôlable par le joueur qui peut accélérer (Left Mouse Button) ou ralentir (Right Mouse Button). Plus on va vite, plus on sillonne la grille rapidement, mais plus on risque de se mordre la queue.

● En plus du risque de se mordre la queue, tout le challenge provient d’un timer de famine qui une fois à zéro provoque également le Game Over. Le joueur est donc incité à aller vite vers la prochaine cachette, mais à ses risques et périls.
        ■ La classe Timer a été revue pour gérer les itérations en plus du temps écoulé.
    
● Pour trouver les cachettes, le serpent repose sur ses sens aiguisés. A mesure qu’il s’approche de la cachette il change de couleur et entend les sons de sa proie.
        ■ On utilise ici principalement une méthode de la classe Grid, qui renvoie le voisinage de Moore sur une distance paramétrable (donc une liste de 8 cellules pour une distance de 1, 25 pour 2, … etc). On soustrait ensuite ces listes avec la méthode Except().

Rappel des Contrôles :

Haut : Z
Gauche : Q
Bas : S
Droite : D
Plus vite : Clic gauche
Plus lentement : Clic Droite
Utiliser Supersens : Clic Molette
Reset : R
Toggle Afficher cachettes (triche) : Enter
