   M S B A M L   `   `   `  ����U  Qnmct.ba.cashlessproject.ui, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullu?clr-namespace:nmct.ba.cashlessproject.ui.ViewModel.Klantsysteem1nmct.ba.cashlessproject.ui.ViewModel.Klantsysteem  5      ?    9nmct.ba.cashlessproject.ui.View.klantsysteem.saldoopladen   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Nvm?clr-namespace:nmct.ba.cashlessproject.ui.ViewModel.Klantsysteem     5        � DataContext  5	      F  @nmct.ba.cashlessproject.ui.ViewModel.Klantsysteem.SaldoOpladenVM  5
   
   .��5      � 
������� � RowDefinitions 5   
   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   .+�5   
   �� $��0��6   $��Saldo opladen��6   $��42(�65   $��Center=�6C   $��0,70,0,0q�6`   �� 5   
   $��18(�6   
���  ��$��0��63   $��2��6W   $��Center=�6k   $��0,0,0,-5q�6�   $��Bottom=�6�   ��6@   �� *Error+6�   �� 5   
   $��Huidig Saldo :��6   $��1��6)   $	��Top=�66   $��Center=�6N   $��
0,10,150,0q�6k   $��20(�6   �� 5   
   $��1��60   $	��Top=�6=   $��Center=�6U   $��
150,10,0,0q�6r   $��20(�6�   ��6   �� *HuidigSaldo+6�   �� 5   
   $��Te opladen :��6   $��1��6'   $��Center=�64   $��Center=�6O   $��0,100,135,0q�6l   $��20(�6�   �� 5   
   $��1��6*   $��Center=�67   $��Center=�6R   $��150,100,0,0q�6o   $	��100��6�   $��50��6�   $��20(�6�   ��6   �� *Bedrag+6�   �� 5   
   $��+��64   $��20(�6@   $��1��6N   $��Center=�6[   $��Center=�6v   $��60��6�   $��60��6�   $��
400,10,0,0q�6�   ��6   �� *SaldoUpCommand+6�   �� 5   
   $��-��66   $��20(�6B   $��1��6P   $��Bottom=�6]   $��Center=�6x   $��60��6�   $��60��6�   $��400,0,0,-50q�6�   ��6   �� *SaldoDownCommand+6�   �� 5    
   $��Nieuw Saldo :��6   $��2��6(   $��center=�65   $��Center=�6P   $��
0,30,150,0q�6m   $��20(�6�   �� 5!   
   $��2��6/   $��Center=�6<   $��Center=�6W   $��
150,30,0,0q�6t   $��20(�6�   ��6   �� *
NieuwSaldo+6�   �� 5#   
   $��
Bevestigen��61   $��18(�6F   $	��200��6T   $	��Top=�6`   $��
0,50,250,0q�6x   $��3��6�   $��Center=�6�   ���	#FF2DA812$��50��6�   $
��Bold"�6�   ��6   �� *NextCommand+6�   �� 5$   
   $��Annuleer��61   $��18(�6D   $	��200��6R   $	��Top=�6^   $��
250,50,0,0q�6v   $��50��6�   $��3��6�   $��Center=�6�   ���	#FFE81D14$
��Bold"�6�   ��6   �� *BackCommand+6�   5(      5)      