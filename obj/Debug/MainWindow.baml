   M S B A M L   `   `   `  ����E  AOSSearcher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:OSSearcher
OSSearcher  5   	       OSSearcher.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:OSSearcher     5   	     � Title$  OS Searcher��5   	   $	��450��6   $	��350��6*   .��5	      � $��0,0,2,2q�6    � RowDefinitions 5
   
   �� 5      $
��188* �6   �� 5      $
��229* �6    � ColumnDefinitions 5   
   �� 5      $	��71* �6   �� 5      $	��99* �6   .+�5   
   �� -   5        $
��Name��5      $��10,10,10,153.11q�6   $��12(�67   $��2��6E   .��6r   File or Folder Name5      �� 5   
   $��File or Folder?��6   $��73,87,40,77q�6+   $��12(�6@   $��1��6N   �� 5   
   -   5        $
��Type��5   d   $��85��6   $
��Left=�6    $��73,111,0,49q�6=   $��1��6T   .@�5      �� $
��File��6   �� 5      $��Folder��6   5      �� 5   
   $ ��Actual or Approximate Name��6   $��10,87,150,77q�66   $��12(�6L   $��2��6Z   �� 5    
   -   5        $��ActualApprox��5    T   $��85��6   $
��Left=�6    $��10,111,0,49q�6=   .@�5!      �� $��Actual��6   �� 5"      $��Approx��6   5#      �� 5&   
   $��10,159,137,5q�6   $��12(�6*   $��2��68   �� 6M   $(�"Search For All or First Occurrence��6Q   �� 6}   �� 6�   $� ��6�   6�   �� 5'   
   -   5        $��
Occurrence��5'   f   $��85��6   $
��Left=�6    $��10,183,0,206q�6=   $��2��6U   .@�5(      �� $��First��6   �� 5)      $��Find All��6   5*      �� 5,   
   -   5         ��Name$ StartingPath��5,      $��10,52,10,111q�6(   $��12(�6>   $��2��6L   $��Leading Directory��6`   6y   �� 5.   
   -   5         ��Name$ SearchButton��5.      $��Search��5/       �� 	ClickMode$ Press=�51      $��92,186,114,10q�52      $	��134��53      $��Center=�54      
����  �$��2��55   %   $��1��69   6F   56      57      