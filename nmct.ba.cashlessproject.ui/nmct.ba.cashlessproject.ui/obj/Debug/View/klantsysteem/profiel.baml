   M S B A M L   `   `   `  ����U  Qnmct.ba.cashlessproject.ui, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullu?clr-namespace:nmct.ba.cashlessproject.ui.ViewModel.Klantsysteem1nmct.ba.cashlessproject.ui.ViewModel.Klantsysteem  5      Dclr-namespace:nmct.ba.cashlessproject.ui.View.klantsysteem.converter6nmct.ba.cashlessproject.ui.View.klantsysteem.converter  5      :    4nmct.ba.cashlessproject.ui.View.klantsysteem.profiel   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Nvm?clr-namespace:nmct.ba.cashlessproject.ui.ViewModel.Klantsysteem     5      RcDclr-namespace:nmct.ba.cashlessproject.ui.View.klantsysteem.converter     5        � DataContext  5      A  ;nmct.ba.cashlessproject.ui.ViewModel.Klantsysteem.ProfielVM  5   
    � 	Resources 5      S  Mnmct.ba.cashlessproject.ui.View.klantsysteem.converter.StringToImageConverter   StringToImageConverter��%   &	          5   
   6#   5      .��5      � 
������� � RowDefinitions 5   
   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��2* �6   �� 5      $��1* �6   .+�5   
   �� $��0��6   $��0,0,50,0q�6   $��	Uitloggen��60   $��Right=�6f   $	��125��6�   $��50��6�   ���	#FFE81D14$
��Bold"�6�   ��6D   �� *LogoutCommand+6�   �� 5   
   $��0��6   $��Uw gegevens��6   $��42(�63   $��Center=�6A   $��Center=�6^   $��0,0,0,0q�6y   �� 5   
   $��1��6   $��Voor- en achternaam:��6   $	��Top=�6<   $��20(�6T   $��Center=�6b   $��
0,10,250,0q�6   �� 5   
   $��1��6   $	��Top=�6:   $��20(�6R   $��Center=�6`   $��
250,10,0,0q�6}   ��6   �� *
FullName+6}   �� 5   
   $��1��6   $��Adres:��6   $��Bottom=�6.   $��20(�6I   $��Center=�6W   $��
0,0,110,30q�6t   �� 5    
   $��1��6   $��Bottom=�67   $��20(�6R   $��Center=�6`   $��
250,0,0,30q�6}   ��6   �� *Adres+6}   �� 5!   
   $��1��6   $��Bottom=�68   $��20(�6S   $��Center=�6a   $��	250,0,0,0q�6~   ��6   �� *Adres2+6~   �� 5#   
   $��2��6   $��Foto:��6   $	��Top=�6-   $��20(�6E   $��Center=�6S   $��
0,10,100,0q�6p   �� 5&   
   $��2��6   $��Saldo:��6   $��Bottom=�6.   $��20(�6I   $��Center=�6W   $��
0,0,110,30q�6t   �� 5'   
   $��2��6   $��Bottom=�67   $��20(�6R   $��Center=�6`   $��
200,0,0,30q�6}   ��6   �� *Saldo+6}   �� 5)   
   $��3��6   $��0,20,0,0q�6   $	��Top=�60   $��Saldo Opladen��6H   $	��200��6�   $��50��6�   ���	#FF2DA812$
��Bold"�6�   ��6`   �� *SaldoCommand+6�   5*      5+      