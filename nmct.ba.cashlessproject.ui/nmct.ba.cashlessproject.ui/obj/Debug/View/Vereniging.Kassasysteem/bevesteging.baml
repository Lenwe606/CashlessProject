   M S B A M L   `   `   `  ����U  Qnmct.ba.cashlessproject.ui, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullu?clr-namespace:nmct.ba.cashlessproject.ui.ViewModel.Kassasysteem1nmct.ba.cashlessproject.ui.ViewModel.Kassasysteem  5   	   I    Cnmct.ba.cashlessproject.ui.View.Vereniging.Kassasysteem.bevesteging   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Nvm?clr-namespace:nmct.ba.cashlessproject.ui.ViewModel.Kassasysteem     5   	     � DataContext  5	      E  ?nmct.ba.cashlessproject.ui.ViewModel.Kassasysteem.BevestigingVM  5
   
   .��5      � 
������� � RowDefinitions 5   
   �� 5      $��1* �6   �� 5      $��3* �6   �� 5      $��1* �6   �� 5      $��1* �6   .+�5   
   �� -   5         ��Name$ lblTitel��5      $��
Afrekening��6"   $��48(�67   $��Center=�6E   $��0,5,0,0q�6b   $��0��6t   �� 5   
    �� DisplayMemberPath$ ProductNaam��60   $��1��6P   $	��400��6]   $��	0,10,0,10q�6i   ��6   �� *Order+5      �� 5   
   $��Totaal:��6   $��2��6"   $��28(�6/   $��Center=�6=   $��
0,20,100,0q�6Z   �� 5   
   $��28(�6+   $��Center=�69   $��2��6V   $��100,20,0,20q�6c   ��6   �� *Totaal+6c   �� 5   
   $��	Afrekenen��61   $��18(�6E   $	��200��6S   $��0,15,220,15q�6_   $��4��6t   $��Center=�6�   ���	#FF2DA812$
��Bold"�6�   ��6   �� *NextCommand+6�   �� 5   
   $��Annuleer��61   $��18(�6D   $	��200��6R   $��220,15,0,15q�6^   $��4��6s   $��Center=�6�   ���	#FFE81D14$
��Bold"�6�   ��6   �� *BackCommand+6�   5      5       