   M S B A M L   `   `   `  ����R  NPrice-comparison-engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:Price_comparison_enginePrice_comparison_engine  5   	   6    0Price_comparison_engine.ConfirmNewPasswordWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:Price_comparison_engine     5   	     � Title$  ConfirmNewPasswordWindow��5   	   $	��350��6*   $	��600��67    � 
ResizeMode$ NoResize=�6C    � 	Resources 5	         FocusVisual �� Property �� Value J� SnapsToDevicePixels �� StrokeDashArray  Button.Static.Background �� Color  Button.Static.Border  Button.Disabled.Background  Button.Disabled.Border  Button.Disabled.Foreground  RoundedButton �� 
TargetType	 �� 
TargetType
 ��Name �� CornerRadius ��Name �� Triggers P� Property P� Value �� 
TargetName��%�  &	        &	 �     &	 �     &	 �     &	     &	 6    &	 Y    7   7  7  7  7  7  �� 5
   
   .�5      ��  � �� 5      �� 5      .R�5      �� $��2q�6$    . #��� ��6J   $��1��6�   $	 1 2R�6�   5      5      5      �� 5   
   $ 	#FFDDDDDD��6;   �� 5   
   $ 	#FF707070��67   �� 5   
   $ 	#FFF4F4F4��6=   �� 5   
   $ 	#FFADB2B5��69   �� 5   
   $ 	#FF838383��6=   �� 5   
   " ��6&   .�5      ��  �@�FocusVisualStyle8   61   �� 5       � ��8  6+   �� 5       � ��8  6,   �� 5       � ��# � ��6+   �� 5       � ��$ 0q�60   �� 5       � ��$ Center=�6;   �� 5       � ��$ Center=�69   �� 5        � ��$ 1q�6(   �� 5!       � �� 5"      �� 5#      "	 ��6&   .R�5$      �� $
 border��6!   $ 20��61   #��z��6C   #��z��6o   #��z��6�    . ._�5%      �� $ contentPresenter��6/   |�.  #��z��6[   #��z��6�   ��. # z 6�   #��z��6  5&       5'      P� 5(       �@��IsDefaulted . . �5)   "   �� $ border��6@   # � ��6T    � ��5*      P� 5+       �@J�IsMouseOver . . �5,   "   �� $ border��6?    � �� �	#EEBB11006S   �� 5-   "   $ border��6@    � �� �	#EEBB11006T   5.      P� 5/       � �� . . �50   "   �� $ border��6?    � �� �	#FFF181686S   �� 51   "   $ border��6@    � �� �	#FFF181686T   52      P� 53       � {� .  . �54   "   �� $ border��6?   8   � ��6S   �� 55   "   $ border��6@   8   � ��6T   �� 56   "   $ contentPresenter��6K   8   � ��6i   57      59      5;      5<      5=      .��5>      � .+�5?   
   �� -   5         ��Name$ SendCodeButton��5?      $��Išsiųsti kodą��6)   $
��Left=�6A   $��210,210,0,0q�6\   $	��Top=�6q   $	��164��6�   ���	#FF015DFF
�������$��Arial)�5B      $��16(�6    #���  6.   $��57��6l   �� 5C   
   -   5         ��Name$ 
ConfirmBox��5C      $y�	Collapsed=�6&   $
��Left=�6=   $��34��6X   $��
53,155,0,0q�6d    �� TextWrapping$
 Wrap=�6x   $�� ��6�   $	��Top=�6�   $	��487��6�   $��20(�6�   �� 5D   
   -   5         ��Name$ Message1��5D      $��Center=�6&   $��64,36,63,201q�6C   $
��Wrap=�6Y   $��84��6m   $	��467��6y   $��Cooper Black)�6�   $��24(�6�    �� TextAlignment$ Center=�6�   $��Center=�6�   �� 6�   $"�Įveskite savo email, kad ga��6�   �� 6	  $1�+lėtume jums išsiųsti patvirtinimo kodą.��6  6?  �� 5E   
   -   5        $ Message2��5E      $y�	Collapsed=�6&   $��Center=�6=   $��64,27,63,185q�6Z   $
��Wrap=�6p   $��Center=�6�   $	��109��6�   $	��467��6�   $��Cooper Black)�6�   $��24(�6�   $ Center=�6�   �� 6�   $.�(Patvirtinkite savo email įvesdami kodą��6�   �� 6,  $�.��60  �� 6;  �� 6G  $%�Kodą rasite savo email pašte.��6K  6s  �� 5F   
   -   5        $ EmailBox��5F      $
��Left=�6$   $��34��6?   $��
53,155,0,0q�6K   $
 Wrap=�6_   $�� ��6s   $	��Top=�6{   $	��487��6�   $��20(�6�   �� 5G   
   -   5        $ ConfirmButton��5G      $y�	Collapsed=�6(   $��Patvirtinti savo email��6?   $
��Left=�6`   $��180,210,0,0q�6{   $	��Top=�6�   $	��222��6�   ���	#FF015DFF
�������$��Arial)�5J      $��16(�6    #���  6.   $��57��6k   5L      5M      