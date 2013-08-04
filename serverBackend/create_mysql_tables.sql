--
-- Table structure for table `commands`
--

CREATE TABLE IF NOT EXISTS `commands` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `from` varchar(255) NOT NULL,
  `to` varchar(255) NOT NULL,
  `read` varchar(255) NOT NULL DEFAULT '66 61 6C 73 65',
  `cmd` text NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `responses`
--

CREATE TABLE IF NOT EXISTS `responses` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `cmdId` varchar(255) NOT NULL,
  `UID` varchar(255) NOT NULL,
  `read` varchar(255) NOT NULL DEFAULT '0',
  `response` text NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE IF NOT EXISTS `users` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `UID` varchar(255) NOT NULL,
  `type` varchar(255) NOT NULL,
  `lastOnlineTime` varchar(255) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;
