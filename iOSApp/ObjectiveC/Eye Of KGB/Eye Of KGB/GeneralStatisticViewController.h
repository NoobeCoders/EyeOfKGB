//
//  GeneralStatisticViewController.h
//  Eye Of KGB
//
//  Created by bu on 21/01/16.
//  Copyright © 2016 Oleg Shamin. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface GeneralStatisticViewController : UIViewController
@property (weak, nonatomic) IBOutlet UIPickerView *sitePicker;
@property (strong, nonatomic) NSMutableArray *sites;
@end
