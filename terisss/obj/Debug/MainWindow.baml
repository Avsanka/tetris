   M S B A M L   `   `   `  ????B  >terisss, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:terisssterisss  5   	       terisss.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:terisss     5   	   -   5          ? Title$  
TetrisGame??5   	   $	??600??6   $	??800??6)   $	??600??5	   	   $	??600??6   
???????$??28(?5
      .??5      ?  ? RowDefinitions 5   
   ?? 5      $
??Auto ?6   ?? 5      $??* ?6    ? ColumnDefinitions 5   
   ?? 5      $??* ?6   ?? 5      $
??auto ?6   ?? 5      $??* ?6   ??5   
   ?? 5       ?? ImageSource$ Assets/background.png??6   .+?5   
   (? $??1??6   $??1??5      $??0,0,0,20q?5      .??5       ?? -   5         ??Name$ 
GameCanvas??5       ???#101010$	??250??5"      $	??500??5#      }?. 5%      5'      ?? 5)   
   -   5         ??Name$ highScoreText??5)      $??0??5*      $??0??5+      $??High Score: ??5,      $
??0,10q?5-       ?? TextAlignment$ Center=?5.      ?? 50   
   -   5        $ 	ScoreText??50      $??0??51      $??2??52      $??Score: ??53      $
??0,10q?54      $ Center=?55      ?? 56   
   $??1??6   $??0??57      $??Center=?58      $??Right=?59      .+?5:      ?? -   5         ??Name$ 	HoldImage??5:      $??20q?5;      $	??125??5<      ?? 5=      -   5        $??Pause??5=      $
??Left=?6%   $	??Top=?6@   $	??140??6X   
????+??$??48??6?   5>      ?? 5@   
   $??1??6   $??2??5A      $??Center=?5B      $
??Left=?5C      .+?5D      ?? $
??Next??6   $ Center=?5E      ?? 5F      -   5        $ 	NextImage??5F      $??20q?5G      $	??125??5H      5I      ? 5K   
   -   5         ?Name$ GameOverMenu??5K      ???	#CC000000$??2??5M      $??3??5N      $y?Hidden=?5O      .+?5P      ?? $??Center=?6   $??Center=?5Q      .+?5R      ?? $??	Game Over??6   $??48(?5S      $ Center=?5T      ?? 5V      -	   5        $ FinalScoreText??5V      $??Score: ??5W      $??36(?5X      $ Center=?5Y      ?? 5[      -
   5        $??Start new game??5[      
???????$??0,20,0,0q?5]      $??5q?5^      5_      5`      5a      ? 5c   
   -   5        $ 	PauseMenu??5c      ???	#CC000000$??2??5e      $??3??5f      $y?Hidden=?5g      .+?5h      ?? $??Center=?6   $??Center=?5i      .+?5j      ?? $??Paused??6   $??48(?5k      $ Center=?5l      ?? 5n      -   5        $??Continue??5n      
???????$??0,20,0,0q?5p      $??5q?5q      5r      5s      5t      5u      5v      