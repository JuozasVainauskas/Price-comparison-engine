   M S B A M L   `   `   `  ����R  NPrice-comparison-engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:Price_comparison_enginePrice_comparison_engine  5   	   2    ,Price_comparison_engine.PasswordChangeWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:Price_comparison_engine     5   	     � Title$  PasswordChangeWindow��5   	   $	��400��6&   $	��600��63    � 
ResizeMode$ NoResize=�6?    � 	Resources 5	         FocusVisual �� Property �� Value J� SnapsToDevicePixels �� StrokeDashArray  Button.Static.Background �� Color  Button.Static.Border  Button.Disabled.Background  Button.Disabled.Border  Button.Disabled.Foreground  RoundedButton �� 
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
   �� -   5         ��Name$ Message1��5?      $��Center=�6&   $��53,60,78,272q�6C   $
��Wrap=�6Y   $��Center=�6m   $��39��6�   $	��463��6�   $��Cooper Black)�6�   $��24(�6�    �� TextAlignment$ Center=�6�   $)��#Sukurkite savo naują slaptažodį.��6�   �� 5@   
   -   5         ��Name$ ChangePasswordBtn��5@      $��Pakeisti slaptažodį��6,   $
��Left=�6J   $��200,252,0,0q�6e   $	��Top=�6z   $	��174��6�   ���	#FF015DFF
�������$��Arial)�5C      $��16(�6    #���  6.   $��51��6r    J� RenderTransformOrigin$ 0.506,1.239&�6~   G� 5D   
   -   5         G�Name$ PasswordBox��5D      $
��Left=�6+   $��
65,134,0,0q�6F   $	��Top=�6Z   $	��451��6r   $��30��6~   $��20(�6�   G� 5E   
   -   5        $ PasswordConfirmBox��5E      $
��Left=�62   $��
65,197,0,0q�6M   $	��Top=�6a   $	��451��6y   $��30��6�   $��20(�6�   �� 5F   
   -   5        $ Message2��5F      $��Center=�6&   $��78,105,162,236q�6C   $
��Wrap=�6[   $��Center=�6o   $��20(�6�   $��30��6�   $	��354��6�   �� 6�   $� ��6�   �� 6�   $	�Sla��6�   �� 6�   $�	ptažodis��6�   �� 6�   $�:��6�   6�   �� 5G   
   -   5        $ Message3��5G      $��Center=�6&   $��78,167,162,174q�6C   $
��Wrap=�6[   $��Center=�6o   $��20(�6�   $��30��6�   $	��354��6�   �� 6�   $� ��6�   �� 6�   $.�(Pakartotinai įveskite savo slaptažodį��6�   �� 6�   $�:��6�   6  5I      5J      