   M S B A M L   `   `   `  ����R  NPrice-comparison-engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:Price_comparison_enginePrice_comparison_engine  5   	   0    *Price_comparison_engine.ConfirmationWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:Price_comparison_engine     5   	     � Title$  ConfirmationWindow��5   	   $	��350��6$   $	��600��61    � 
ResizeMode$ NoResize=�6=    � 	Resources 5	         FocusVisual �� Property �� Value J� SnapsToDevicePixels �� StrokeDashArray  Button.Static.Background �� Color  Button.Static.Border  Button.Disabled.Background  Button.Disabled.Border  Button.Disabled.Foreground  RoundedButton �� 
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
   �� -   5         ��Name$ Patvirtinti��5?      $��Patvirtinti savo email��6&   $
��Left=�6G   $��180,210,0,0q�6b   $	��Top=�6w   $	��222��6�   ���	#FF015DFF
�������$��Arial)�5B      $��16(�6    #���  6.   $��57��6k   �� 5C   
   -   5         ��Name$ 
ConfirmBox��5C      $
��Left=�6&   $��34��6A   $��
53,155,0,0q�6M    �� TextWrapping$
 Wrap=�6a   $�� ��6u   $	��Top=�6}   $	��487��6�   $��20(�6�   �� 5D   
   -   5         ��Name$ message��5D      $��Center=�6%   $��62,41,65,171q�6B   $
��Wrap=�6X   $��Center=�6l   $	��109��6�   $	��467��6�   $��Cooper Black)�6�   $��24(�6�    �� TextAlignment$ Center=�6�   �� 6�   $.�(Patvirtinkite savo email įvesdami kodą��6�   �� 6  $�.��6  �� 6#  �� 6/  $%�Kodą rasite savo email pašte.��63  6[  5E      5F      