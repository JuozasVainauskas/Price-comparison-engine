   M S B A M L   `   `   `  ����R  NPrice-comparison-engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:Price_comparison_enginePrice_comparison_engine  5   	   )    #Price_comparison_engine.ItemsWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:Price_comparison_engine     5   	   -   5          � Title$  Prekės��5   	   $	��600��6   $	��930��6%   $	��600��6R   $	��930��6b   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $
��227* �6   �� 5      $	��33* �6   �� 5      $
��532* �6   .+�5   
   �� -   5         ��Name$ 
rectangle1��5      ���	#FF8CD479$
��Left=�69   $	��570��6T   
���   �$	��Top=�6p   $	��188��6�   $��0,0,0,-1q�6�   �� 5   
   -   5        $ 
rectangle2��5      ���	#FF0FA943$��43��69   $��
188,0,-2,0q�6E   
���   �$	��Top=�6h   $	��606��6�   $
��Left=�6�   $��3��6�   &   System.Windows.Controls.DataGrid  5   
   -   5        $��dataGrid��5   /      AutoGenerateColumns .     CanUserAddRows . $��193,219,10,10q�6y   $��3��6�   ��6U   �� *TestBinding+   Columns 5      4  .System.Windows.Controls.DataGridTemplateColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Image��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ SizeToCells 68      
IsReadOnly . 	   CellTemplate	 5      �� 5      .R�5      �� $��
imageClick��6L   -   5        $	��100��5   ?   ��6$   �� *	Picture+6^   5      5      0  *System.Windows.Controls.DataGridTextColumn  5      $ 
Pardavejas��6%    . $ 70 6k   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6K   ��  �� Path$ Seller�6x     5      $ Preke��6%    . $	 300 6e   
 6G   �� $
 Name�6s     5      $ Kaina��6%    . $	 100 6f   
 6G   �� $ Price�6t     5      $ Nuoroda��6)   	 5      �� 5       .R�5!      ��  ��Name$ 
linkButton��6%   -   5        ���	#FF36B04D �� ContentStringFormat$  ��5!   P   .��5"   "   �� ��6,   �� *Link+.�6I   .�5#   &   �� �� 61   .�66   Eiti6<   6C   5$   #   5%      5&      5(        5)      $ 
Išsaugoti��6)   	 5*      �� 5+      .R�5,      �� $ 
saveButton��6%   -   5        ���	#FF36B04D$  ��5,   P   .��5-   "   �� �� 6-   .�62   
Išsaugoti6=   6D   5.      5/      51      53      � 54   
   -   5         �Name$ 
wrapPanel1��54      $
��Left=�6(   $	��100��6C   $��
207,88,0,0q�6P   $	��Top=�6d   $	��100��6|   $��3��6�   55      56      